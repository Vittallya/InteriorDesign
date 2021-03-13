using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace BL
{
    public class StylesService
    {
        protected readonly AllDbContext context;

        public StylesService(AllDbContext context)
        {
            this.context = context;
        }


        /// <summary>
        /// Получить стили
        /// </summary>
        /// <param name="name">имя, по которому будут отбираться стили</param>
        /// <returns></returns>
        public async Task<ObservableCollection<Style>> GetStyles(string name = null)
        {
            await context.Styles.LoadAsync();


            return new ObservableCollection<Style>(name != null ? context.Styles.Where(s => s.Name.Contains(name)) : context.Styles);
        }
    }


    public class EditStylesService: StylesService, IControlledAction
    {
        private readonly ICurrentUserService userService;

        public EditStylesService(AllDbContext context, ICurrentUserService userService):base(context)
        {
            this.userService = userService;
        }

        public int ActionIndex => 1;
    }
}
