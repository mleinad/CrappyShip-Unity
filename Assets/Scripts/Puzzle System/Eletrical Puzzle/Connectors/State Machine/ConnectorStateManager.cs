using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorStateManager : MonoBehaviour
{
   ConnectorBaseState currentState;
   public ConnectedState connectedState = new ConnectedState();
   public DroppedState droppedState = new DroppedState();
   public HeldState heldState = new HeldState();

   ModuleBase currentBaseModule;
   Rigidbody rigibody;
   DragNDrop dragNDrop;
   ISignalModifier signalModifier;

   private void Awake()
   {
      rigibody = GetComponent<Rigidbody>();
      dragNDrop = GetComponent<DragNDrop>();
      signalModifier = GetComponent<ISignalModifier>();
   }

   void Start()
   {
      currentState = droppedState;
   }

   void Update()
   {
      currentState.UpdateState(this);
   }

   public void SwitchState(ConnectorBaseState newState)
   {
      currentState.ExitState(this);
      currentState = newState;
      currentState.EnterState(this);
   }
   
   
   public DragNDrop GetDragNDrop() => dragNDrop;
   public ISignalModifier GetSignalModifier() => signalModifier;
  
   public Rigidbody GetRigidbody() => rigibody;
   public ModuleBase GetCurrentBase() => currentBaseModule;
   public ModuleBase GetCurrentBase(ModuleBase newBase) => currentBaseModule = newBase;
}
