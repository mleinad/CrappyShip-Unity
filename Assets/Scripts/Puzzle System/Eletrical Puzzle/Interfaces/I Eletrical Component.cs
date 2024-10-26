using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEletricalComponent
{
        public int GetSignal();
        public List<IEletricalComponent> GetAdjacencies();
}
