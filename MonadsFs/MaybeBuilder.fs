namespace MonadFs

module OptionHelpers =
    let inline (|?) (a: 'a option) b = if a.IsSome then a.Value else b

module Builders = 
    type MaybeBuilder() =

        member this.Bind(x, f) = 
            match x with
            | None -> None
            | Some a -> f a

        member this.Return(x) = 
            Some x

        member this.ReturnFrom x = x
   
    let maybe = new MaybeBuilder()