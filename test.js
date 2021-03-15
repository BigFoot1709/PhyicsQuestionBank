function myFunction() {
    const {
        MongoClient
    } = require("mongodb");

    const uri = "mongodb+srv://BigFoot:Digger1709@cluster0.yn5ot.mongodb.net/QuestionBank1?retryWrites=true&w=majority";
    const databaseName = "QuestionBank1";
    const collectionName = "QuestionBank1";

    MongoClient.connect(uri, {
        useNewUrlParser: true
    }, (error, client) => {
        if (error) {
            return console.log("Connection failed for some reason");
        }
        console.log("Connection established - All well");
        const db = client.db(databaseName);
        const collection = db.collection(collectionName);

        var questiontext = document.getElementById("email").value;
        var car = document.getElementById("age").value;

        var myobj = {
            name: questiontext,
            imageBin: car
        };

        collection.insertOne(myobj, function (err, res) {
            console.log("1 document inserted");
        });

        client.close();

    });
}