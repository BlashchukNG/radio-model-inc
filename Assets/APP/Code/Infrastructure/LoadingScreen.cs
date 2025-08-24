using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Infrastructure
{
	public sealed class LoadingScreen : MonoBehaviour
	{
		[SerializeField] private GraphicRaycaster _raycaster;
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private float _duration = 0.5f;


		private void Awake() => _canvasGroup.alpha = 0;

		public void Show()
		{
			_raycaster.enabled = true;
			_canvasGroup.DOFade(1, _duration);
		}

		public void Hide()
		{
			_canvasGroup.DOFade(0, _duration)
			            .OnComplete(() => _raycaster.enabled = false);
		}
	}
}