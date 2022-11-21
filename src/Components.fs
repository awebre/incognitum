namespace App

open System
open App.OAuth
open Feliz
open Feliz.Router
open Thoth.Fetch
open Thoth.Json

type Components =
    static member baseUrl = "https://localhost:8081"
    
    [<ReactComponent>]
    static member MainPage() =
        let (domain, setDomain) = React.useState("")
        let submit() =
            promise {
                let oauthRelativeUrl = Router.formatPath("oauth", "redirect")
                let data =
                    {
                        client_name = "incognitum"
                        redirect_uris= $"%s{Components.baseUrl}/%s{oauthRelativeUrl}"
                        scopes= "read"
                        website= Components.baseUrl
                    }

                let! res = Fetch.tryPost<RegisterAppRequest, RegisterAppResponse>($"https://%s{domain}/api/v1/apps", data, caseStrategy = CamelCase)
                return res
            }
            
        Html.div [
            Html.label [
                prop.htmlFor "domain"
                prop.text "Instance: "
            ]
            Html.input [
                prop.name "domain"
                prop.title "Instance Domain"
                prop.placeholder "mastodon.social"
                prop.value domain
                prop.onChange setDomain
            ]
            Html.button [
                prop.text "Submit"
                prop.onClick (fun _ -> submit() |> ignore)
            ]
        ]
        
    /// <summary>
    /// The simplest possible React component.
    /// Shows a header with the text Hello World
    /// </summary>
    [<ReactComponent>]
    static member HelloWorld() = Html.h1 "Hello World"
        
        
    /// <summary>
    /// A stateful React component that maintains a counter
    /// </summary>
    [<ReactComponent>]
    static member Counter() =
        let (count, setCount) = React.useState(0)
        Html.div [
            Html.h1 count
            Html.button [
                prop.onClick (fun _ -> setCount(count + 1))
                prop.text "Increment"
            ]
        ]

    /// <summary>
    /// A React component that uses Feliz.Router
    /// to determine what to show based on the current URL
    /// </summary>
    [<ReactComponent>]
    static member Router() =
        
        let (currentUrl, updateUrl) = React.useState(Router.currentPath())
        Console.WriteLine(currentUrl)
        React.router [
            router.pathMode
            router.onUrlChanged (updateUrl)
            router.children [
                match currentUrl with
                | [] -> Components.MainPage()
                | [ "hello" ] -> Components.HelloWorld()
                | [ "counter" ] -> Components.Counter()
                | otherwise -> Html.h1 "Not found"
            ]
        ]