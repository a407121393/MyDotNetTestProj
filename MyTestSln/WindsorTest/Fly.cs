using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    public class Fly : IFly
    {
        private string _name;
        private IShot _shot;

        public Fly(string name, IShot shot)
        {
            this._name = name;
            this._shot = shot;
        }
        void IFly.Fly()
        {
            Console.WriteLine(string.Format("I am {0}.I can Fly!!!", this._name));
            _shot.Shot();
        }
    }
}