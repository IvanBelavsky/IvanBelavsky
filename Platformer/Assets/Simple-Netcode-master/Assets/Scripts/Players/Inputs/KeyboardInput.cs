using UnityEngine;


namespace Players
{
    public class KeyboardInput : IInput
    {
        public float Direction()
        {
            return Input.GetAxis("Horizontal");
        }


        public bool Jump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}