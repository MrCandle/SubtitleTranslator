namespace SubtitleTranslator.Controllers

open Microsoft.AspNetCore.Mvc
open Google.Cloud.Translation.V2
open System.IO
open System.Collections.Generic
open Microsoft.AspNetCore.Mvc
open System.Text.RegularExpressions

[<Route("api/[controller]")>]
[<ApiController>]
type TranslationController () =
    inherit ControllerBase()

    [<HttpGet>]
    member __.Get() =
        let readLines (filePath:string) = seq {
            use sr = new StreamReader (filePath)
            while not sr.EndOfStream do
                yield sr.ReadLine ()
        }
        
        let str =
            "/home/mrcandle/Downloads/Station.19.S02E10.720p.HDTV.x264-AVS-es.srt" 
            |> readLines
            |> Seq.map (fun line -> 
                match line with
                    |  x when Regex.Match(x, @"^\d{2}:\d{2}:\d{2},\d{3}.*\d{2}:\d{2}:\d{2},\d{3}$").Success -> line
                    |  y when Regex.Match(y, @"^\d+$").Success -> line
                    |  z when Regex.Match(z, @"^\n").Success -> line
                    | _ -> "reescribir esto!"
                )
            |> String.concat "\n"
        
        
        
        //let client = TranslationClient.Create()
        //let result = client.TranslateText("Hello World", LanguageCodes.Spanish)
        //ActionResult<string>(result.TranslatedText)
        ActionResult<string>(str)



    [<HttpPost>]
    member __.Post([<FromBody>] value:string) =
        ()
