using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCSharp.Assets.Boss.Script.Interface
{
    public interface IBoss
    {
        
        public void MeleeAttack();
        public void RangeAttack();
        public void Confusion();
        public void Rage();
    }
}
