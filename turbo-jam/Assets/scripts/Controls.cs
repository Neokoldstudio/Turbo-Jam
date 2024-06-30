//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/scripts/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""989f5ed2-0a57-4655-af71-4b6ab37b1610"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""df6f3c65-9771-4246-b90d-ac675441c350"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""lookMouse"",
                    ""type"": ""Value"",
                    ""id"": ""9bc2a820-3c25-4ccb-9734-675ac678ca7f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""lookJoystick"",
                    ""type"": ""Value"",
                    ""id"": ""092bed89-32bc-4920-a670-8b89b821b3c4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""hit"",
                    ""type"": ""Button"",
                    ""id"": ""7bb783c8-f505-4b26-971b-1092ef4af402"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""parry"",
                    ""type"": ""Button"",
                    ""id"": ""e9b4217b-a0d7-4dac-bed7-3db2450917fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""ad8d78db-fd39-4b5f-b356-a8adb50be48d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""throwWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""f99b35a4-0510-42d8-923a-9fd4fabffb84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""openInventory"",
                    ""type"": ""Button"",
                    ""id"": ""58febedd-d4b0-4331-b238-397b8b1819d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""dodge"",
                    ""type"": ""Button"",
                    ""id"": ""491c4813-6b1c-4989-8db6-14926c6770aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard"",
                    ""id"": ""7ad235b0-44e1-4e3a-9473-d8237137e2bd"",
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
                    ""id"": ""0cfd9f64-6b34-4f46-a663-03d8ac4b0b94"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""25f77792-9f4c-4b99-ae3c-a01ae3afb863"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""26130238-4ece-4373-b023-a426432b6f0a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c4a405be-7785-4a44-8433-01bafbadaff4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""0fcb9a00-2f89-4773-8c92-0e03ca46dae5"",
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
                    ""id"": ""70002915-5242-4d17-bc82-8729462c94e3"",
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
                    ""id"": ""d81ef4fa-92e8-4cde-8a13-98dbd6facda4"",
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
                    ""id"": ""cf2f64a3-d02e-4b81-a05a-1dcbbda0393d"",
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
                    ""id"": ""50f6173c-3c41-4485-8a50-b65ec084af13"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""controller"",
                    ""id"": ""b70bc595-da6d-478c-a55a-9cfa11dbf772"",
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
                    ""id"": ""a9656bff-c65f-40b7-bdc7-4bf5bfc9e504"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""32bc4852-0e93-4c72-a5a4-c4fe05600d27"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""84af28f2-42d3-4a5b-89ae-c17dfa0f9c3b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""67c15bb0-6e60-44ef-8b58-7c128bdf3a2e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""84de5406-88d0-4b6e-93a3-225f715f2a1c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa46c390-6101-48c7-8b89-2b39a35a2dec"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""240c3ee7-6561-43fa-8060-0da335a1ed11"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f42f3f84-5dde-46bd-82a6-7a967a63d4eb"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f38b57f5-9c9b-44aa-a986-12b5da1158ab"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f65a8da6-b79b-4175-8ff0-edbecca87f07"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""070cbc81-2f35-4ede-9f64-6ef6f29aad2c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""lookJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee8fb6bf-26fe-4dc9-a5df-edb1e6bd41c0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""lookMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a754218-2032-4211-9bda-6855b1309331"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b75c3285-d598-4ac6-9dbd-8b8854bdc831"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""throwWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2037f0a-6241-41e2-be61-52e2dd5f6325"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""openInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8aba855a-9777-4e82-8168-7531918d54bc"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f8ef8f1-3065-4723-a0b9-2abc6f1a099a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_lookMouse = m_Player.FindAction("lookMouse", throwIfNotFound: true);
        m_Player_lookJoystick = m_Player.FindAction("lookJoystick", throwIfNotFound: true);
        m_Player_hit = m_Player.FindAction("hit", throwIfNotFound: true);
        m_Player_parry = m_Player.FindAction("parry", throwIfNotFound: true);
        m_Player_interact = m_Player.FindAction("interact", throwIfNotFound: true);
        m_Player_throwWeapon = m_Player.FindAction("throwWeapon", throwIfNotFound: true);
        m_Player_openInventory = m_Player.FindAction("openInventory", throwIfNotFound: true);
        m_Player_dodge = m_Player.FindAction("dodge", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_lookMouse;
    private readonly InputAction m_Player_lookJoystick;
    private readonly InputAction m_Player_hit;
    private readonly InputAction m_Player_parry;
    private readonly InputAction m_Player_interact;
    private readonly InputAction m_Player_throwWeapon;
    private readonly InputAction m_Player_openInventory;
    private readonly InputAction m_Player_dodge;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @lookMouse => m_Wrapper.m_Player_lookMouse;
        public InputAction @lookJoystick => m_Wrapper.m_Player_lookJoystick;
        public InputAction @hit => m_Wrapper.m_Player_hit;
        public InputAction @parry => m_Wrapper.m_Player_parry;
        public InputAction @interact => m_Wrapper.m_Player_interact;
        public InputAction @throwWeapon => m_Wrapper.m_Player_throwWeapon;
        public InputAction @openInventory => m_Wrapper.m_Player_openInventory;
        public InputAction @dodge => m_Wrapper.m_Player_dodge;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @lookMouse.started += instance.OnLookMouse;
            @lookMouse.performed += instance.OnLookMouse;
            @lookMouse.canceled += instance.OnLookMouse;
            @lookJoystick.started += instance.OnLookJoystick;
            @lookJoystick.performed += instance.OnLookJoystick;
            @lookJoystick.canceled += instance.OnLookJoystick;
            @hit.started += instance.OnHit;
            @hit.performed += instance.OnHit;
            @hit.canceled += instance.OnHit;
            @parry.started += instance.OnParry;
            @parry.performed += instance.OnParry;
            @parry.canceled += instance.OnParry;
            @interact.started += instance.OnInteract;
            @interact.performed += instance.OnInteract;
            @interact.canceled += instance.OnInteract;
            @throwWeapon.started += instance.OnThrowWeapon;
            @throwWeapon.performed += instance.OnThrowWeapon;
            @throwWeapon.canceled += instance.OnThrowWeapon;
            @openInventory.started += instance.OnOpenInventory;
            @openInventory.performed += instance.OnOpenInventory;
            @openInventory.canceled += instance.OnOpenInventory;
            @dodge.started += instance.OnDodge;
            @dodge.performed += instance.OnDodge;
            @dodge.canceled += instance.OnDodge;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @lookMouse.started -= instance.OnLookMouse;
            @lookMouse.performed -= instance.OnLookMouse;
            @lookMouse.canceled -= instance.OnLookMouse;
            @lookJoystick.started -= instance.OnLookJoystick;
            @lookJoystick.performed -= instance.OnLookJoystick;
            @lookJoystick.canceled -= instance.OnLookJoystick;
            @hit.started -= instance.OnHit;
            @hit.performed -= instance.OnHit;
            @hit.canceled -= instance.OnHit;
            @parry.started -= instance.OnParry;
            @parry.performed -= instance.OnParry;
            @parry.canceled -= instance.OnParry;
            @interact.started -= instance.OnInteract;
            @interact.performed -= instance.OnInteract;
            @interact.canceled -= instance.OnInteract;
            @throwWeapon.started -= instance.OnThrowWeapon;
            @throwWeapon.performed -= instance.OnThrowWeapon;
            @throwWeapon.canceled -= instance.OnThrowWeapon;
            @openInventory.started -= instance.OnOpenInventory;
            @openInventory.performed -= instance.OnOpenInventory;
            @openInventory.canceled -= instance.OnOpenInventory;
            @dodge.started -= instance.OnDodge;
            @dodge.performed -= instance.OnDodge;
            @dodge.canceled -= instance.OnDodge;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLookMouse(InputAction.CallbackContext context);
        void OnLookJoystick(InputAction.CallbackContext context);
        void OnHit(InputAction.CallbackContext context);
        void OnParry(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnThrowWeapon(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
    }
}
