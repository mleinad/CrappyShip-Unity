using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEletricalComponent
{
        public int GetSignal();
        public void SetSignal(int value);
        public List<IEletricalComponent> GetAdjacencies();
}
