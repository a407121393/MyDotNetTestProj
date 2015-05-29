using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    public interface IShot
    {
        void Shot();
    }

    public class Shot : IShot
    {

        void IShot.Shot()
        {
            Console.WriteLine("Shot!!!");
        }
    }
}
