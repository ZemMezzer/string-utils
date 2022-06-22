using StringUtils.Extensions;
using UnityEngine;

namespace StringUtils.Assets.Demo
{
    public class StringUtilsDemo : MonoBehaviour
    {
        private void Start()
        {
            string outputString = "Hello World";
            
            Debug.Log($"Original String: {outputString}, Hash Code: {outputString.GetHashCode().ToString()}");
            
            outputString.ReplaceAt(0, 'D');
            outputString.ReplaceAll('l', 'D');
            outputString.ReplaceChar('o', 'D');
            
            Debug.Log($"Original String After Change: {outputString}, Hash Code: {outputString.GetHashCode().ToString()}");
        }
    }
}
