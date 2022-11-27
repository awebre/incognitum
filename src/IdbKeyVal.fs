module App.IdbKeyVal

open Fable.Core.JS

type IKeyValErr =
    abstract member errorMessage:string

type IKeyVal =
    abstract get : string -> Promise<string>
    abstract set : string * string -> Promise<string>
