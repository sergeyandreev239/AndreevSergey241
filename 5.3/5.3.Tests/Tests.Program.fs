﻿module Tests

    open Logic
    open NUnit.Framework
    open FsUnit
    open System.IO
    open System.Runtime.Serialization.Formatters.Binary

    let testDictionary = Map.ofList [
        "Jeff", "89119974729";
        "Fred", "89119974729";
        "Mary", "89119977777" ]

    [<Test>]
    let ``check that the output of the entire database works correctly``() =
        consoleReader testDictionary "SELECT * FROM Dictionary" |> should equal (testDictionary, testDictionary)

    [<Test>]
    let ``check that the search by name works correctly``() =
        consoleReader testDictionary "SELECT * FROM Dictionary WHERE name = Fred" |> should equal (Map.ofList ["Fred", "89119974729"], testDictionary)

    [<Test>]
    let ``check that the search by phone works correctly``() =
        consoleReader testDictionary "SELECT * FROM Dictionary WHERE phone = 89119974729" |> should equal (Map.ofList ["Jeff", "89119974729"; "Fred", "89119974729"], testDictionary)

    [<Test>]
    let ``check that adding to the dictionary works correctly``() =
        consoleReader testDictionary "INSERT INTO Dictionary VALUES (Slava, 88005553535)" |> should equal ((Map.add "Slava" "88005553535") testDictionary, (Map.add "Slava" "88005553535") testDictionary)

    [<Test>]
    let ``check that writing to the file works correctly``() =
        (consoleReader ((Map.add "Slava" "88005553535") testDictionary) "WRITE Dictionary TO FILE") |> should equal (Map.ofList["recording completed successfully",""], (Map.add "Slava" "88005553535") testDictionary)

    [<Test>]
    let ``check that reading from the file works correctly``() =
        (consoleReader testDictionary "READ Dictionary FROM FILE") |> should equal ((Map.add "Slava" "88005553535") testDictionary, (Map.add "Slava" "88005553535") testDictionary)

    [<Test>]
    let ``check what happens when the wrong command``() =
        (consoleReader testDictionary "The cake is a lie") |> should equal (Map.ofList["Incorrect command",""], testDictionary)

    