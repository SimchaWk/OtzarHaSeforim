document.getElementById("shelfForm").addEventListener("submit", function (event) {
    var requiredWidth = @ViewBag.RequiredWidth;
    var requiredHeight = @ViewBag.RequiredHeight;
    var highShelf = document.getElementById("HighShelf").value;
    var widthShelf = document.getElementById("WidthShelf").value;

    if (widthShelf < requiredWidth || highShelf < requiredHeight) {
        event.preventDefault();
        alert("Shelf dimensions are not sufficient. Please enter valid dimensions.");
    }
});
