using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncVirtualization.Library;

namespace AsyncVirtualization.Tests {
    public class TestElement : IElement {
        public int ID { get; private set; }

        public TestElement(int id) {
            ID = id;
        }
    }
}
