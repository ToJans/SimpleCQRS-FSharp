﻿module Events

    open System

    (*
    #r "System.CoreEx.dll"
    #r "System.Reactive.dll" 
    *)
    open System.Collections.Generic

    // Events implemented as discriminated union. 
    // If you use a big solution, change to a base type 
    // or just use many Subjects and concatenate / merge with LINQ 
    type Event = 
    | InventoryItemCreated      of Guid * string
    | InventoryItemDeactivated  of Guid
    | InventoryItemRenamed      of Guid * string
    | ItemsCheckedInToInventory of Guid * int
    | ItemsRemovedFromInventory of Guid * int
    with 
        override x.ToString() = 
            match x with
            | InventoryItemCreated(i,n) -> "Item " + n + " created (id:" + i.ToString() + ")"
            | InventoryItemDeactivated(i) -> "Item deactivated (id:" + i.ToString() + ")"
            | InventoryItemRenamed(i,n) -> "Item renamed to " + n + " created (id:" + i.ToString() + ")"
            | ItemsCheckedInToInventory(i,c) -> "Check-in " + c.ToString() + " of item (id:" + i.ToString() + ")"
            | ItemsRemovedFromInventory(i,c) -> "Removed " + c.ToString() + " of item (id:" + i.ToString() + ")"
    
    ///Used just to notify others if anyone would be interested
    let eventBusSubject = new Subject<Event>()
    let public EventBus = eventBusSubject :> IObservable<Event>