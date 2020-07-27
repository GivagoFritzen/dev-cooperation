// GENERATED AUTOMATICALLY FROM 'Assets/Project/Scripts/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Instance"",
            ""id"": ""3086b864-262e-4208-8893-422d21ab50b5"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""fa7b8369-308c-40f6-970d-d13da4dfc6e1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""25a306f4-3088-4748-b3e3-ffc533af24bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""229bf422-b04f-4070-8582-b8f0df6c380b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DistanceAttack"",
                    ""type"": ""Button"",
                    ""id"": ""86add14b-5a36-4203-9722-ce710c2af2fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""0b826e3b-4c9c-4d23-a32a-0e8e5300384a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""74f70fae-011c-408b-85a9-e1e363cfa5b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondUp"",
                    ""type"": ""Button"",
                    ""id"": ""412f2e3c-5b83-4b7b-9504-3ae7c40d9bcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondDown"",
                    ""type"": ""Button"",
                    ""id"": ""99aeff37-6cd4-464c-abd5-df10bcee64ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""a6a0aa4c-a5d6-49b9-be74-1a829f4ba980"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""ARROW"",
                    ""id"": ""405ed1a5-7451-4370-8bef-fe0a80aa59b6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a4a5744e-6203-4908-9750-6f45251a9c47"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c90a9c84-f5dc-4a55-a714-39fbf5aadf86"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""74bb9100-bb96-410a-9f81-222b60d07d58"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ab3ee2e6-8f35-4e48-af75-5af9b67a0e91"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e3bc6379-0a60-4b55-b346-94258f574155"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5582d8d-28fd-42d5-b54b-cd6a047dc37f"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f99c3ee7-45f3-4059-96e2-195b1a5094f8"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DistanceAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""639f2001-f6d1-4104-a23b-2a8697834458"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1822161-ff48-44f6-aaea-1554f60e9e87"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a356e672-c848-4d86-a69d-ccf3eab5ed5b"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33860330-ce57-4620-9567-349f79ad86d0"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Instance
        m_Instance = asset.FindActionMap("Instance", throwIfNotFound: true);
        m_Instance_Movement = m_Instance.FindAction("Movement", throwIfNotFound: true);
        m_Instance_Menu = m_Instance.FindAction("Menu", throwIfNotFound: true);
        m_Instance_Inventory = m_Instance.FindAction("Inventory", throwIfNotFound: true);
        m_Instance_DistanceAttack = m_Instance.FindAction("DistanceAttack", throwIfNotFound: true);
        m_Instance_Map = m_Instance.FindAction("Map", throwIfNotFound: true);
        m_Instance_Action = m_Instance.FindAction("Action", throwIfNotFound: true);
        m_Instance_SecondUp = m_Instance.FindAction("SecondUp", throwIfNotFound: true);
        m_Instance_SecondDown = m_Instance.FindAction("SecondDown", throwIfNotFound: true);
        m_Instance_Newaction = m_Instance.FindAction("New action", throwIfNotFound: true);
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

    // Instance
    private readonly InputActionMap m_Instance;
    private IInstanceActions m_InstanceActionsCallbackInterface;
    private readonly InputAction m_Instance_Movement;
    private readonly InputAction m_Instance_Menu;
    private readonly InputAction m_Instance_Inventory;
    private readonly InputAction m_Instance_DistanceAttack;
    private readonly InputAction m_Instance_Map;
    private readonly InputAction m_Instance_Action;
    private readonly InputAction m_Instance_SecondUp;
    private readonly InputAction m_Instance_SecondDown;
    private readonly InputAction m_Instance_Newaction;
    public struct InstanceActions
    {
        private @PlayerControls m_Wrapper;
        public InstanceActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Instance_Movement;
        public InputAction @Menu => m_Wrapper.m_Instance_Menu;
        public InputAction @Inventory => m_Wrapper.m_Instance_Inventory;
        public InputAction @DistanceAttack => m_Wrapper.m_Instance_DistanceAttack;
        public InputAction @Map => m_Wrapper.m_Instance_Map;
        public InputAction @Action => m_Wrapper.m_Instance_Action;
        public InputAction @SecondUp => m_Wrapper.m_Instance_SecondUp;
        public InputAction @SecondDown => m_Wrapper.m_Instance_SecondDown;
        public InputAction @Newaction => m_Wrapper.m_Instance_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Instance; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InstanceActions set) { return set.Get(); }
        public void SetCallbacks(IInstanceActions instance)
        {
            if (m_Wrapper.m_InstanceActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMovement;
                @Menu.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMenu;
                @Inventory.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnInventory;
                @DistanceAttack.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnDistanceAttack;
                @DistanceAttack.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnDistanceAttack;
                @DistanceAttack.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnDistanceAttack;
                @Map.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMap;
                @Action.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnAction;
                @SecondUp.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnSecondUp;
                @SecondUp.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnSecondUp;
                @SecondUp.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnSecondUp;
                @SecondDown.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnSecondDown;
                @SecondDown.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnSecondDown;
                @SecondDown.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnSecondDown;
                @Newaction.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_InstanceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @DistanceAttack.started += instance.OnDistanceAttack;
                @DistanceAttack.performed += instance.OnDistanceAttack;
                @DistanceAttack.canceled += instance.OnDistanceAttack;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
                @SecondUp.started += instance.OnSecondUp;
                @SecondUp.performed += instance.OnSecondUp;
                @SecondUp.canceled += instance.OnSecondUp;
                @SecondDown.started += instance.OnSecondDown;
                @SecondDown.performed += instance.OnSecondDown;
                @SecondDown.canceled += instance.OnSecondDown;
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public InstanceActions @Instance => new InstanceActions(this);
    public interface IInstanceActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnDistanceAttack(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnSecondUp(InputAction.CallbackContext context);
        void OnSecondDown(InputAction.CallbackContext context);
        void OnNewaction(InputAction.CallbackContext context);
    }
}
