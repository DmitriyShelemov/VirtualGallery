function GalleryRoom(options) {

    var defaults = {
        scene: null,
        scale: 1,
        floor: {
            texture: "@Links.Content.Img.Textures.stariy_parket_jpg",
            scale: 0.5
        },
        wall: {
            texture: "@Links.Content.Img.Textures.ashley_ruAQ50911_resize_JPG",
            scale: 0.5
        },
        roof: {
            texture: "@Links.Content.Img.Textures.ashley_ruAQ50911_resize_JPG",
            scale: 0.5
        },
        points: [
         {
             x: 0,
             z: 0
         },
         {
             x: 0,
             z: 10
         },
         {
             x: 10,
             z: 10
         },
         {
             x: 10,
             z: 0
         }],
        height: 300,
        enters: []
    };

    var that = this;
    var settings = $.extend({}, defaults, options);
    
    var bitmaps = [];

    this.draw = function () {
        // load the wall texture before anything else
        var loader = new Phoria.Preloader();
        bitmaps.push(new Image());
        loader.addImage(bitmaps[0], settings.floor.texture);
        bitmaps.push(new Image());
        loader.addImage(bitmaps[1], settings.wall.texture);
        bitmaps.push(new Image());
        loader.addImage(bitmaps[2], settings.roof.texture);
        loader.onLoadCallback($.proxy(function () {

            var points = $.map(settings.points, function (point, index) {
                return $.extend({ x: 0, y: 0, z: 0 }, point);
            });
            // floor
            that.drawPlane(0, points, settings.scene);

            // sides
            for (var i = 0; i < points.length - 1; i++) {
                that.drawSide(1, points[i], points[i + 1]);
            }
            that.drawSide(1, points[points.length - 1], points[0]);

            // roof
            that.drawPlane(2, $.map(points, function (point, index) {
                return { x: point.x, y: point.y + settings.height, z: point.z };
            }), settings.scene);
        }, this));
    };

    this.drawSide = function (texture, point1, point2) {
        that.drawPlane(texture, [
            point1,
            { x: point1.x, y: point1.y + settings.height, z: point1.z },
            { x: point2.x, y: point2.y + settings.height, z: point2.z },
            point2
        ], settings.scene);
    };

    this.drawPlane = function (texture, points, scene) {
        var s = settings.scale || 1;
        points = $.map(points, function(point, index) {
            return { x: point.x * s, y: point.y * s, z: point.z * s };
        });
        
        var edges = [];
        for (var i = 0; i < points.length - 1; i++) {
            edges.push({ a: i, b: i + 1 });
        }

        edges.push({ a: points.length - 1, b: 0 });
        
        var minPoint = points[0];
        var maxPoint = points[0];
        for (var j = 0; j < points.length; j++) {
            var pnt = points[j];
            minPoint = {
                x: Math.min(minPoint.x, pnt.x),
                y: Math.min(minPoint.y, pnt.y),
                z: Math.min(minPoint.z, pnt.z)
            };
            maxPoint = {
                x: Math.max(maxPoint.x, pnt.x),
                y: Math.max(maxPoint.y, pnt.y),
                z: Math.max(maxPoint.z, pnt.z)
            };
        }

        var drawPolygon = function (squarePoints) {
            var entity = Phoria.Entity.create({
                points: squarePoints,
                edges: edges,
                polygons: [{ vertices: [0, 1, 2, 3], texture: texture }],
                style: {
                    shademode: "plain",
                    doublesided: true
                }
            });
            entity.textures = bitmaps;
            scene.graph.push(entity);
        };

        var x = 0, y = 0, z = 0, step = 2;
        if (minPoint.x == maxPoint.x) {
            for (z = minPoint.z; z < maxPoint.z; z += step) {
                for (y = minPoint.y; y < maxPoint.y; y += step) {
                    drawPolygon([{ x: minPoint.x, y: y, z: z },
                                { x: minPoint.x, y: y + step, z: z },
                                { x: minPoint.x, y: y + step, z: z + step },
                                { x: minPoint.x, y: y, z: z + step }]);
                }
            }
        }
        else if (minPoint.y == maxPoint.y) {
            for (x = minPoint.x; x < maxPoint.x; x += step) {
                for (z = minPoint.z; z < maxPoint.z; z += step) {
                    drawPolygon([{ x: x, y: minPoint.y, z: z },
                                { x: x + step, y: minPoint.y, z: z },
                                { x: x + step, y: minPoint.y, z: z + step },
                                { x: x, y: minPoint.y, z: z + step }]);
                }
            }
        }
        else if (minPoint.z == maxPoint.z) {
            for (x = minPoint.x; x < maxPoint.x; x += step) {
                for (y = minPoint.y; y < maxPoint.y; y += step) {
                    drawPolygon([{ x: x, y: y, z: minPoint.z },
                                { x: x + step, y: y, z: minPoint.z },
                                { x: x + step, y: y + step, z: minPoint.z },
                                { x: x, y: y + step, z: minPoint.z }]);
                }
            }
        }
    };

    this.draw();

    return this;
};