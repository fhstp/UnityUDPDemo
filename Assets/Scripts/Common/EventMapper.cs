using UnityEngine;
using UnityEngine.Events;

namespace Ac.At.FhStp.UnityUDPDemo
{

    public abstract class EventMapper<TIn, TOut> : MonoBehaviour
    {

        [SerializeField] private UnityEvent<TOut> onMapped;


        public void OnEvent(TIn value) => 
            onMapped.Invoke(Map(value));

        protected abstract TOut Map(TIn value);

    }

}