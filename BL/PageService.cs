using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reflection;
namespace BL
{

    public enum AnimateTo
    {
        None, Left, Rigth
    };

    public class PageService
    {
        public event Action<Page, AnimateTo> PageChanged;


        public void ChangePage<TPage>() where TPage : Page, new()
        {
            var page = new TPage();
            OnChangePage(page, AnimateTo.None);

        }

        public void ChangePage<TPage>(AnimateTo animate) where TPage : Page, new()
        {
            var page = new TPage();
            OnChangePage(page, animate);

        }

        public void ChangePage<TPage>(int poolIndex) where TPage : Page, new()
        {
            ChangePage<TPage>(AnimateTo.None, poolIndex);
        }

        public void ChangePage<TPage>(AnimateTo animate, int poolIndex) where TPage: Page, new()
        {

            Page page = null;



            if (poolIndex != ActualPool && _pool.ContainsKey(poolIndex))
            {
                page = _pool[poolIndex].FirstOrDefault(x => x.GetType() == typeof(TPage));
            }

            bool isNew = false;

            if(page == null)
            {
                page = new TPage();
                isNew = true;
            }

            ChangePage(page, animate, poolIndex, isNew);

        }

        public void ChangePage<TPage>(TPage target, AnimateTo animateTo, int poolIndex, bool isNew) where TPage : Page, new()
        {

            OnChangePage(target, animateTo);

            if(poolIndex != ActualPool)
            {
                if(_poolHistory.Count == 0 || !_poolHistory.Contains(poolIndex))
                    _poolHistory.Add(poolIndex);

                ActualPool = poolIndex;
            }

            
            
            if (!_pool.ContainsKey(poolIndex))
            {
                _pool.Add(poolIndex, new List<Page>());
            }

            if(isNew)
                _pool[poolIndex].Add(target);

        }

        public void OnChangePage<TPage>(TPage target, AnimateTo animateTo) where TPage : Page, new()
        {
            PageChanged?.Invoke(target, animateTo);
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
                    int oldPool = _poolHistory[index];
                    ActualPool = oldPool;

                    ChangeToLastByPool(oldPool, animatePlaying ? AnimateTo.Rigth : AnimateTo.None);
                }
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
