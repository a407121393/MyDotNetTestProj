using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCommonTest
{
    public partial class TestServerViewState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var dir = new DirectoryInfo(this.Server.MapPath("~/App_Data/ViewState/"));
            if (!dir.Exists)
                dir.Create();
            else
            {
                var nt = DateTime.Now.AddHours(-1);
                foreach (var f in dir.GetFiles())
                {
                    if (f.CreationTime < nt)
                        f.Delete();
                }
            }
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            var viewStateID = (string)((Pair)base.LoadPageStateFromPersistenceMedium()).Second;
            var stateStr = (string)Cache[viewStateID];
            if (stateStr == null)
            {
                var fn = Server.MapPath(@"App_Data/ViewState/" + viewStateID);
                stateStr = File.ReadAllText(fn);
            }
            return new ObjectStateFormatter().Deserialize(stateStr);
        }

        protected override void SavePageStateToPersistenceMedium(object state)
        {
            var value = new ObjectStateFormatter().Serialize(state);
            var viewStateID = (DateTime.Now.Ticks + (long)this.GetHashCode()).ToString(); //产生离散的id号码
            var fn = Server.MapPath(@"App_Data/ViewState/" + viewStateID);
            File.WriteAllText(fn, value);

            Cache.Insert(viewStateID, value);
            base.SavePageStateToPersistenceMedium(viewStateID);
        }

    }
}