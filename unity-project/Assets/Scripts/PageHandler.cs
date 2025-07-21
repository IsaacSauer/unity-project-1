using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game
{
	public class PageHandler : Singleton<PageHandler>
	{
		[SerializeField] private List<Page> _pages = new List<Page>();

		private Dictionary<string, Page> _pageDictionary = new Dictionary<string, Page>();
		private Page _currentPage;

		private void OnValidate()
		{
			_pages = GetComponentsInChildren<Page>(true).ToList();
		}

		protected override void Awake()
		{
			base.Awake();
			foreach (var mono in _pages)
			{
				if (mono is Page page)
				{
					_pageDictionary.Add(page.ID, page);
				}
			}
			_currentPage = _pageDictionary[Homepage.HomePageId];
		}

		public bool ToPage(string id)
		{
			if (_pageDictionary.TryGetValue(id, out var page))
			{
				return ToPage(page);
			}
			Debug.LogWarning($"Page ID {id} not found");
			return false;
		}

		public bool ToPage(Page page)
		{
			page.transform.SetAsLastSibling();
			page.gameObject.SetActive(true);
			page.transform.localPosition = Vector3.zero;

			if (_currentPage != null)
			{
				_currentPage.transform.SetAsLastSibling();
				var cachedPage = _currentPage;

				cachedPage.transform.DOLocalMoveY(Screen.height * 2f, 0.5f)
					.SetRecyclable(true)
					.SetEase(Ease.OutSine)
					.OnComplete(() =>
					{
						cachedPage.gameObject.SetActive(false);
						_currentPage = page;
					});
			}
			else
			{
				_currentPage = page;
			}

			return true;
		}
	}
}