using System;
using System.Collections.Generic;

namespace model.di
{
	public sealed class DIContainer
	{
		private readonly DIContainer _parent_container;
		private readonly Dictionary<(string, Type), DIEntry> _entries_map = new();
		private readonly HashSet<(string, Type)> _resolutions_cache = new();

		public DIContainer(DIContainer parentContainer = null)
		{
			_parent_container = parentContainer;
		}

		public DIEntry RegisterFactory<T>(Func<DIContainer, T> factory)
		{
			return RegisterFactory(null, factory);
		}

		public DIEntry RegisterFactory<T>(string tag, Func<DIContainer, T> factory)
		{
			var key = (tag, typeof(T));

			if (_entries_map.ContainsKey(key))
			{
				throw new Exception(
					$"DI: Factory with tag {key.Item1} and type {key.Item2.FullName} has already registered");
			}

			var diEntry = new DIEntry<T>(this, factory);

			_entries_map[key] = diEntry;

			return diEntry;
		}

		public void RegisterInstance<T>(T instance)
		{
			RegisterInstance(null, instance);
		}

		public void RegisterInstance<T>(string tag, T instance)
		{
			var key = (tag, typeof(T));

			if (_entries_map.ContainsKey(key))
			{
				throw new Exception(
					$"DI: Instance with tag {key.Item1} and type {key.Item2.FullName} has already registered");
			}

			var diEntry = new DIEntry<T>(instance);

			_entries_map[key] = diEntry;
		}

		public T Resolve<T>(string tag = null)
		{
			var key = (tag, typeof(T));

			if (_resolutions_cache.Contains(key))
			{
				throw new Exception($"DI: Cyclic dependency for tag {key.tag} and type {key.Item2.FullName}");
			}

			_resolutions_cache.Add(key);

			try
			{
				if (_entries_map.TryGetValue(key, out var diEntry))
				{
					return diEntry.Resolve<T>();
				}

				if (_parent_container != null)
				{
					return _parent_container.Resolve<T>(tag);
				}
			}
			finally
			{
				_resolutions_cache.Remove(key);
			}

			throw new Exception($"Couldn't find dependency for tag {tag} and type {key.Item2.FullName}");
		}

		public void Dispose()
		{
			var entries = _entries_map.Values;

			foreach (var entry in entries)
			{
				entry.Dispose();
			}
		}
	}
}