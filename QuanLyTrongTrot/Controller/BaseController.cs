
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Controller
{
    using Model;
    using QuanLyTrongTrot.Model;

    class UpdateContext : EditContext
    {
        public string Message { get; set; }
    }
    class BaseController : System.Mvc.Controller
    {
        public Provider Provider => Provider.Current;
        public object Error(int code, string message)
        {
            return new { ErrorCode = code, ErrorMessage = message };
        }
        public virtual object Index() => View();
    }

    class DataController<T> : BaseController
    {
        public Type EntityType => typeof(T);
        protected virtual T CreateEntity() => (T)Activator.CreateInstance(EntityType);
        protected virtual string GetProcName()
        {
            var name = EntityType.Name;
            if (name.ToLower().StartsWith("view"))
                name = name.Substring(4);
            return name;
        }
        public override object Index()
        {
            return View(Provider.Select<T>());
        }
        public virtual object Delete(T entity)
        {
            return View(new EditContext(entity, EditActions.Delete));
        }
        public virtual object Edit(T entity)
        {
            return View(new EditContext(entity));
        }
        public virtual object Add()
        {
            return View(new EditContext(CreateEntity(), EditActions.Insert));
        }

        protected virtual void TryInsert(T e) { }
        protected virtual void TryUpdate(T e) { }
        protected virtual void TryDelete(T e) { }
        protected UpdateContext UpdateContext { get; private set; }
        public virtual object Update(EditContext context)
        {
            var e = (T)context.Model;
            UpdateContext = new UpdateContext { Action = context.Action };

            switch (context.Action)
            {
                case EditActions.Delete: TryDelete(e); break;
                case EditActions.Update: TryUpdate(e); break;
                case EditActions.Insert: TryInsert(e); break;
            }

            if (UpdateContext.Message != null)
                return Error(1, UpdateContext.Message);
            return RedirectToAction("index");
        }

        protected void Exec(params object[] values)
        {
            var args = new List<object> { (int)UpdateContext.Action };
            foreach (var v in values)
            {
                var a = "NULL";
                if (v != null)
                {
                    a = $"'{v}'";
                }
                if (v is string)
                {
                    a = 'N' + a;
                }
                args.Add(a);
            }

            var sql = $"exec update{GetProcName()} {string.Join(",", args)}";
            Provider.Exec(sql);
        }

    }
}
