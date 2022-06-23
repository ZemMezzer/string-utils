using StringUtils.Utils;
using StringUtils.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace StringUtils.Assets.Demo
{
    public class StringUtilsDemo : MonoBehaviour
    {
        [SerializeField] private Text label;
        
        private void Start()
        {
            string outputString = "Hello World";
            
            Debug.Log($"Original String: {outputString}, Hash Code: {outputString.GetHashCode().ToString()}");
            
            outputString.ReplaceAt(0, 'D');
            outputString.ReplaceAll('l', 'D');
            outputString.ReplaceChar('o', 'D');
            
            Debug.Log($"Original String After Change: {outputString}, Hash Code: {outputString.GetHashCode().ToString()}");


            StringCreator stringCreator = new StringCreator(12);

            stringCreator += "Hello";
            stringCreator += " ";
            stringCreator += "World";

            stringCreator.Insert(stringCreator.Length, "!");

            Debug.Log(stringCreator.ToString());
            label.text = stringCreator;
        }
    }
}
