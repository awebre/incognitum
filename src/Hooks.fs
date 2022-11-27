namespace App

open Feliz
open App.OAuth;


type Hooks =
    
    [<Hook>]
    static member useSavedInstances() =
        let (instances, setInstances) = React.useState(List.empty<SuccessfulAppRegistration>)
        
        let addInstance(newInstance:SuccessfulAppRegistration) = newInstance :: instances |> setInstances
        
        (instances, addInstance)

