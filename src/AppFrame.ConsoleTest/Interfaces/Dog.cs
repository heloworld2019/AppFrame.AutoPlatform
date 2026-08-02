using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.ConsoleTest.Interfaces
{
    internal class Dog : IAnimal
    {
        public Dog()
        {
            Name = "dog";
        }
        public string Name { get;private set;}
    }
}
