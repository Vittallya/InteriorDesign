﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
namespace BL
{

    public enum AnimateTo
    {
        None, Left, Rigth
    };

    public class PageService
    {
        public event Action<Page, AnimateTo> PageChanged;
        public PagesBinder PagesBinder { get; set; } = new PagesBinder();

        public void ChangePage(Type pageType, AnimateTo animate = AnimateTo.None)
        {
            var page = Activator.CreateInstance(pageType) as Page;
            OnChangePage(page, animate);

        }
        public void ChangePage<TPage>(AnimateTo animate = AnimateTo.None, object dataContext = null) where TPage : Page, new()
        {
            var page = new TPage();
            if (dataContext != null)
                page.DataContext = dataContext;
            OnChangePage(page, animate);

        }
        public void ChangePage<TPage>(int poolIndex, AnimateTo animate = AnimateTo.None, object dataContext = null) where TPage: Page, new()
        {

            Page page = null;
            
            bool isExist = false;
            bool other = poolIndex != ActualPool;
            bool poolContains = _pool.ContainsKey(poolIndex);
            bool hasSame = poolContains && _pool[poolIndex].Any(x => x.GetType() == typeof(TPage));

            if (other)
            {
                ActualPool = poolIndex;

                if (hasSame)
                {
                    page = _pool[poolIndex].FirstOrDefault(x => x.GetType() == typeof(TPage));
                    if (dataContext != null)
                        page.DataContext = dataContext;
                    isExist = true;
                }
            }

            if (!_poolHistory.Contains(poolIndex))
                _poolHistory.Add(poolIndex);


            if(!isExist)
            {
                page = new TPage();
                if (dataContext != null)
                    page.DataContext = dataContext;

                if (!poolContains)
                {
                    _pool.Add(poolIndex, new List<Page>());
                }
                _pool[poolIndex].Add(page);

            }

            OnChangePage(page, animate);
        }
        public void OnChangePage<TPage>(TPage target, AnimateTo animateTo) where TPage : Page, new()
        {
            PageChanged?.Invoke(target, animateTo);
            PagesBinder.CheckBind<TPage>();
        }

        #region ByType
        

        public void ChangePageByType<TPage, Type>(AnimateTo animateTo = AnimateTo.None, object dataCont = null) where TPage : Page, new()
        {
            var target = new TPage();
            if (dataCont != null)
            {
                target.DataContext = dataCont;
            }
            OnChangePageByType<TPage, Type>(target, animateTo);
        }

        public void ChangePageByType<TPage, Type>(int poolIndex, AnimateTo animateTo = AnimateTo.None, object dataCont = null) where TPage : Page, new()
        {
            var target = new TPage();
            if (dataCont != null)
            {
                target.DataContext = dataCont;
            }
            ChangePageByType<TPage, Type>(target, AnimateTo.None, poolIndex);            
        }

        

        public void ChangePageByType<TPage, Type>(TPage target, AnimateTo animateTo, int poolIndex) where TPage : Page, new()
        {
            OnChangePageByType<TPage, Type>(target, animateTo);

            if (!_pool.ContainsKey(poolIndex))
            {
                _pool.Add(poolIndex, new List<Page>());
            }
            _pool[poolIndex].Add(target);
        }

        public void OnChangePageByType<TPage, Type>(TPage target, AnimateTo animateTo) where TPage : Page, new()
        {
            if (_subs.ContainsKey(typeof(Type)))
            {
                _subs[typeof(Type)]?.Invoke(target, animateTo);
            }
        }

        Dictionary<Type, Action<Page, AnimateTo>> _subs = new Dictionary<Type, Action<Page, AnimateTo>>();

        public void SubscribeByType<T>(Action<Page, AnimateTo> method)
        {
            if (!_subs.ContainsKey(typeof(T)))
            {
                _subs.Add(typeof(T), method);
            }
        }
        #endregion

        List<Page> _history = new List<Page>();

        List<int> _poolHistory = new List<int>();

        public int ActualPool { get; private set; } = -1;

        Dictionary<int, List<Page>> _pool = new Dictionary<int, List<Page>>();

        public void Back(int poolIndex, bool animatePlaying = true)
        {
            if (_pool.ContainsKey(poolIndex))
            {

                if (_pool[poolIndex].Count > 1)
                {
                    Page last = _pool[poolIndex].LastOrDefault();
                    Page target = _pool[poolIndex].Skip(_pool[poolIndex].Count - 2).FirstOrDefault();

                    _pool[poolIndex].Remove(last);
                    PageChanged?.Invoke(target, animatePlaying ? AnimateTo.Rigth : AnimateTo.None);
                }
                else if(_poolHistory.IndexOf(poolIndex) > 0)
                {
                    int index = _poolHistory.IndexOf(poolIndex) - 1;
                    ActualPool = _poolHistory[index];

                    ChangeToLastByPool(ActualPool, animatePlaying ? AnimateTo.Rigth : AnimateTo.None);
                }
            }

        }

        public void Back<TPage>(int poolIndex, bool animatePlaying = true) where TPage : Page, new()
        {
            if (_pool.ContainsKey(poolIndex) &&
                _pool[poolIndex].Count > 1 &&
                _pool[poolIndex].Any(x => x.GetType() == typeof(TPage)))
            {
                var list = _pool[poolIndex];

                var target = list.Find(x => x.GetType() == typeof(TPage));
                int index = list.IndexOf(target);


                _pool[poolIndex].RemoveRange(index + 1, list.Count - index - 1);
                OnChangePage(target, animatePlaying ? AnimateTo.Rigth : AnimateTo.None);

            }

        }

        public void ChangeToLastByPool(int index, AnimateTo animateTo = AnimateTo.None)
        {
            if (_pool.ContainsKey(index) && _pool[index].Count > 0)
            {
                var target = _pool[index].Last();
                OnChangePage(target, animateTo);
            }
        }

        public void ChangeToLastByActualPool(AnimateTo animateTo = AnimateTo.None)
        {            
            var target = _pool[ActualPool].Last();
            OnChangePage(target, animateTo);            
        }

        public void AddPageToPool<TPage>(int poolId) where TPage: Page, new()
        {
            if (_pool.ContainsKey(poolId))
            {
                var target = new TPage();
                _pool[poolId].Add(target);
            }
        }

        public void ClearHistoryByPool(int index)
        {
            if (_pool.ContainsKey(index))
            {
                _pool.Remove(index);
            }
        }

        public void ClearAllPools()
        {
            _pool.Clear();
        }

        public bool HasPoolByIndex(int index)
        {
            return _pool.ContainsKey(index);
        }
        public bool HasActualPool()
        {
            return _pool.Count > 0 && _pool.ContainsKey(ActualPool);
        }
    }
}
