using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ac.At.FhStp.UnityUDPDemo
{

    public class OpenScene : MonoBehaviour
    {

        public void WithName(string name) =>
            SceneManager.LoadScene(name);

    }

}