
/**
 * a code for diplaying the dot of Lon
 * and  Lat on the map.
 * */
function drawCircle(ctx, lat, lon) {
    ctx.canvas.width = window.innerWidth;   //sets the canvas on whole page
    ctx.canvas.height = window.innerHeight;

    y = (lat + 90) * (window.innerHeight / 180);
    x = (lon + 180) * (window.innerWidth / 360);

    //draw red circle at the current plane location
    ctx.fillStyle = "red";
    ctx.arc(x, y, 5, 0, 2 * Math.PI);
    ctx.fill();
    ctx.stroke();
}

//initiating a path, according to the starting lat and lon values
function setLine(ctx, lat, lon) {

    y = (lat + 90) * (window.innerHeight / 180);
    x = (lon + 180) * (window.innerWidth / 360);

    ctx.beginPath();
    ctx.moveTo(x, y);
    ctx.strokeStyle = "red";
    ctx.lineWidth = 2;
}