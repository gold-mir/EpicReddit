var heckCount = 1;
var cat;
var ಠ_ಠ = function(){
    alert ("Unknown error");
}

var test = function(){
    console.log("test");
}

var writeHeckDisplay = function(data){
    $("#heckdisplay").empty();
    data.forEach(function(item){
        var times = item.split("Heck the number ")[1];
        $("#heckdisplay").append(`<li>${"🤔".repeat(times)}</li>`);
    })
}
var tempOutput = [];
var otherOutput = [];

function getAllCardsFromScryfallQuery(queryString, finished, cards = []){

    var ajaxcall = {
        url: queryString,
        type: "GET",
        dataType: "json",
        success: function(result)
        {
            cards.push.apply(cards, result.data);
            if(result.has_more){
                console.log(`Got ${cards.length} results. Getting next page.`);
                setTimeout(function(){getAllCardsFromScryfallQuery(result.next_page, finished, cards);}, 50);
            } else {
                finished(cards);
            }
        },
        error: ಠ_ಠ
    }
    $.ajax(ajaxcall);
}

function compareQueries(results1, results2){
    var output = [];
    for(var i = 0; i < results1.length; i++){
        var r1item = results1[i];
        let foundMatch = false;

        for(var j = 0; j < results2.length; j++){
            var r2item = results2[j];
            if(r1item.oracle_id === r2item.oracle_id){
                results2.splice(j, 1);
                foundMatch = true;
                break;
            }
        }

        if(!foundMatch){
            output.push(r1item);
        }
    }

    return output;
}

var allCardsNotReprint = [];
var allCardsWithReprint = [];
$(document).ready(function(){

    $("button[name=send-cat]").click(function(event){
        var query = encodeURI($("input[name=scryfall-query-input]").val());
        getAllCardsFromScryfallQuery(`https://api.scryfall.com/cards/search?q=${query}`, function(result){
            var toSend = [];
            result.forEach(function(item){
                toSend.push(JSON.stringify(item));
            });
            $.ajax({
                url: "/mtg",
                type: "POST",
                dataType: "json",
                data: {cards: toSend},
                success: function(data){
                    $("#card-display").text("");
                    console.log("\nGot the following cards from the server:")
                    data.forEach(function(item){
                        console.log(item);
                        $("#card-display").append(`<li>${item}</li>`);
                    })
                },
                error: ಠ_ಠ
            });
        });
    });

    $("#heck-number-update").submit(function(event){
        event.preventDefault();
        var newHecks = parseInt($("#heck-number-update input[name=heck-count]").val());
        if(!isNaN(newHecks)){
            if(newHecks > 0){
                heckCount = newHecks;
            }
        }
        console.log(`Heck Count: ${heckCount}`);
    });
    $("button[name=random-number-button]").click(function(event){
        $.ajax({
            url: "/rand",
            type: "GET",
            success: function(data){
                console.log(data);
            },
            error: ಠ_ಠ
        });
    });
    $("button[name=heck-button]").click(function(event){
        $.ajax({
            url: "/heck",
            type: "POST",
            data: {
                numberOfHecks: heckCount,
                data: {name: "fred", occupation: "some guy", dogs: "3", action: "hi", stuff: [1, 2, 3, 4]}
            },
            success: function(data){
                cat = data;
            },
            error: ಠ_ಠ
        });
    });
});
