using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    public class DbContextLoader
    {
        public bool IsLoaded { get; private set; }

        public AllDbContext _context;

        private Thread t;
        public void StartLoading()
        {
            t = new Thread(() =>
            {
                _context = new AllDbContext();
                _context.Services.Load();
                IsLoaded = true;
                t = null;
            });
            t.Start();
        }

        public void Abort()
        {
            t?.Abort();
            _context?.Dispose();
            IsLoaded = false;
        }
    }
}
