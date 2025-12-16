using DG.Tweening;
using UnityEngine;

namespace Utils
{
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(CanvasGroup))]
	public sealed class CanvasShowHideController : MonoBehaviour
	{
		private Canvas _canvas;
		private CanvasGroup _canvasGroup;

		private void Awake()
		{
			TryGetComponent(out _canvas);
			TryGetComponent(out _canvasGroup);

			_canvasGroup.alpha = 0;
		}


		public void Show(float duration)
		{
			_canvas.enabled = true;
			_canvasGroup.DOFade(1, duration);
		}

		public void Hide(float duration)
		{
			_canvasGroup.DOFade(0, duration)
			            .OnComplete(() => { _canvas.enabled = false; });
		}
	}
}