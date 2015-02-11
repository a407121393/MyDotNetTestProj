using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCommonTest
{
    public partial class TestViewStateMac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                for (int i = 0; i < 100; i++)
                {
                    dic.Add(i.ToString(), i.ToString() + "hello world");
                }
                GridView1.DataSource = dic;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("a");
        }
    }
}