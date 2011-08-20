MapItPrices.Store = function () {
    var myPrivateVariable = "I can be accessed only from within MapItPrices.Store";
    var myPrivateMethod = function () {
        MapItPrices.log("I can be accessed only from within MapItPrices.Store");
    };

    return {
        myPublicProperty: "I'm accessible as MapItPrices.Store.myPublicProperty",
        myPublicMethod: function() {
            
        };
} ();