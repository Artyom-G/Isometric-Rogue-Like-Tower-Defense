// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""MainGameplay"",
            ""id"": ""19f1f394-eb57-43a2-98bf-48afa1b70bb2"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""0c07d708-cddc-40c5-a1c6-e93f0bdfc755"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseAction1"",
                    ""type"": ""Button"",
                    ""id"": ""f6accf54-a895-4fb6-9f43-1ec933de1ebb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseAction2"",
                    ""type"": ""Button"",
                    ""id"": ""52f26b5d-e791-482e-b856-b629621abbd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6c407139-4ec1-45a2-8e69-9b7cf82362b6"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a351a0f-2c2c-4415-a316-a2586d1999a4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAction1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45b5f021-6523-416a-a334-ce2f6ef6f324"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAction2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainGameplay
        m_MainGameplay = asset.FindActionMap("MainGameplay", throwIfNotFound: true);
        m_MainGameplay_MousePosition = m_MainGameplay.FindAction("MousePosition", throwIfNotFound: true);
        m_MainGameplay_MouseAction1 = m_MainGameplay.FindAction("MouseAction1", throwIfNotFound: true);
        m_MainGameplay_MouseAction2 = m_MainGameplay.FindAction("MouseAction2", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // MainGameplay
    private readonly InputActionMap m_MainGameplay;
    private IMainGameplayActions m_MainGameplayActionsCallbackInterface;
    private readonly InputAction m_MainGameplay_MousePosition;
    private readonly InputAction m_MainGameplay_MouseAction1;
    private readonly InputAction m_MainGameplay_MouseAction2;
    public struct MainGameplayActions
    {
        private @InputSystem m_Wrapper;
        public MainGameplayActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_MainGameplay_MousePosition;
        public InputAction @MouseAction1 => m_Wrapper.m_MainGameplay_MouseAction1;
        public InputAction @MouseAction2 => m_Wrapper.m_MainGameplay_MouseAction2;
        public InputActionMap Get() { return m_Wrapper.m_MainGameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainGameplayActions set) { return set.Get(); }
        public void SetCallbacks(IMainGameplayActions instance)
        {
            if (m_Wrapper.m_MainGameplayActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMousePosition;
                @MouseAction1.started -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMouseAction1;
                @MouseAction1.performed -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMouseAction1;
                @MouseAction1.canceled -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMouseAction1;
                @MouseAction2.started -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMouseAction2;
                @MouseAction2.performed -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMouseAction2;
                @MouseAction2.canceled -= m_Wrapper.m_MainGameplayActionsCallbackInterface.OnMouseAction2;
            }
            m_Wrapper.m_MainGameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseAction1.started += instance.OnMouseAction1;
                @MouseAction1.performed += instance.OnMouseAction1;
                @MouseAction1.canceled += instance.OnMouseAction1;
                @MouseAction2.started += instance.OnMouseAction2;
                @MouseAction2.performed += instance.OnMouseAction2;
                @MouseAction2.canceled += instance.OnMouseAction2;
            }
        }
    }
    public MainGameplayActions @MainGameplay => new MainGameplayActions(this);
    public interface IMainGameplayActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseAction1(InputAction.CallbackContext context);
        void OnMouseAction2(InputAction.CallbackContext context);
    }
}
