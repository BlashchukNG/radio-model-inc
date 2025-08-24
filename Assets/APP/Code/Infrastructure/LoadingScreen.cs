using DG.Tweening;
using UnityEngine;

namespace Infrastructure
{
	public sealed class LoadingScreen : MonoBehaviour
	{
		[SerializeField] private float _duration = 0.5f;

		private CanvasGroup _canvasGroup;
		
		private void Awake() => _canvasGroup.alpha = 0;

		public void Show() => _canvasGroup.DOFade(1, _duration);

		public void Hide() => _canvasGroup.DOFade(0, _duration);
	}
}