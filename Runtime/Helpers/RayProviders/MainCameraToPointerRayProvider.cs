using System;
using UnityEngine;
public class MainCameraToPointerRayProvider : MonoBehaviour, IRayProvider
{
    private InputProvider _inputProvider;

    public Ray GetRay()
    {
        // todo: is it really necessary to create an instance?
        // can you make all the input methods statics instead?
        _inputProvider = new InputProvider();
        return Camera.main.ScreenPointToRay(_inputProvider.PointerInput());
	}
}

