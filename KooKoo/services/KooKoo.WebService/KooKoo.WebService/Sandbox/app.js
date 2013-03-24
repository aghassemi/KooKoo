// Models
Story = Backbone.Model.extend({
    url: "../api/stories"
});

Stories = Backbone.Collection.extend({
    model: Story,
    url: "../api/stories"
});

/*places */

Place = Backbone.Model.extend({
});

Places = Backbone.Collection.extend({
    fetch: function (options) {

        var me = this;

        var googlePlaceCallback = function( results, status ) {
            me.add(results);
        }

        var pyrmont = new google.maps.LatLng(options.data.latitude, options.data.longitude);

        var request = {
            location: pyrmont,
            types: placeTypes,
            rankBy: google.maps.places.RankBy.DISTANCE
        };

        var service = new google.maps.places.PlacesService(document.createElement('div'));
        service.nearbySearch(request, googlePlaceCallback);
    }
});

StoryListView = Backbone.View.extend({

    tagName:'ul',

    initialize:function () {
        this.model.bind("reset", this.render, this);
        this.model.on('add', this.add, this);
    },

    add: function( story ) {
        $(this.el).prepend(new StoryListItemView({ model: story }).render().el);
    },

    render: function (eventName) {
        $(this.el).empty();
        _.each(this.model.models, function (story) {
            $(this.el).append(new StoryListItemView({ model: story }).render().el);
        }, this);
        return this;
    }

});

StoryListItemView = Backbone.View.extend({

    tagName: "li",

    template: _.template($('#tpl-story-item').html()),

    render: function (eventName) {
        $(this.el).html(this.template(this.model.toJSON()));
        return this;
    }

});

FileThumbnailView = Backbone.View.extend({

    el: '#file-thumb-container',

    initialize: function () {

    },

    render: function (eventName) {
        var reader = new FileReader();
        var me = this;
        reader.onload = function (e) {
            var image = $('#file-thumb-container img');
            
            image.attr("src", e.target.result);
            me.$el.show();
        }

        var file = document.getElementById('input-file').files[0];
        reader.readAsDataURL(file);
        return this;
    },

    hide: function () {
        this.$el.hide();
    }

});

PostStoryView = Backbone.View.extend({

    el: '#post-story-container',

    initialize: function () {
        this.postButton = $('#btn-story-post');
        this.stroyTextInput = $('#story-input');
        var fileInput = $('#input-file');
        $('#btn-add-file').click(function () {
            fileInput.click();
        });
        this.thumbView = new FileThumbnailView();
        
        var me = this;
        fileInput.change(function () {
            me.thumbView.render();
        })


        this.placeList = new Places();
        this.placeListView = new PlaceListView({ model: this.placeList });

        App.Location.Get().on('success', function (coords) {
            me.placeList.fetch(
                {
                    data: {
                        latitude: coords.latitude,
                        longitude: coords.longitude
                    }
                }
            );
        });
    },

    events: {
        "click #btn-story-post": "post",
    },

    uploadFile: function( storyId ) {
        var file = document.getElementById('input-file').files[0];
        var me = this;
        var data = new FormData();
        data.append("file", file);

        $.ajax({
            url: '/api/upload/' + encodeURIComponent(storyId),
            type: "POST",
            data: data,
            processData: false,
            contentType: false,
            success: function () {
                App.storyList.fetch();
            }
        });

        me.thumbView.hide();
    },

    post: function () {
        
        var me = this;

        App.Location.Get().on('success', function ( coords ) {
            var story = new Story();
            var latitude = null;
            var longitude = null;
            latitude = coords.latitude;
            longitude = coords.longitude;
            story.set(
                {
                    storytext: me.stroyTextInput.val(),
                    latitude: latitude,
                    longitude: longitude
                }
            );
            story.save(
             {},
             {
                 success: function (model, response) {
                     if ($('#input-file').val()) {
                         me.uploadFile(model.get('Id'));
                     } else {
                         App.storyList.add( model );
                     }
                 }
             }
             );
            me.stroyTextInput.val('');
        });
        

    },

    render: function (eventName) {
        var me  = this;


        $('#places-container').html(this.placeListView.render().el);
        return this;
    }

});


PlaceListView = Backbone.View.extend({

    tagName: 'ul',

    initialize: function () {
        this.model.bind("reset", this.render, this);
        this.model.on('add', this.add, this);
    },

    add: function (place) {
        $(this.el).prepend(new PlaceListItemView({ model: place }).render().el);
    },

    render: function (eventName) {
        $(this.el).empty();
        _.each(this.model.models, function (place) {
            $(this.el).append(new PlaceListItemView({ model: place }).render().el);
        }, this);
        return this;
    }

});

PlaceListItemView = Backbone.View.extend({

    tagName: "li",

    template: _.template($('#tpl-place-item').html()),

    render: function (eventName) {
        $(this.el).html(this.template(this.model.toJSON()));
        return this;
    }

});

// Router
var AppRouter = Backbone.Router.extend({

    routes: {
        "": "home"
    },

    home: function () {
        App.storyList = new Stories();
        this.storyListView = new StoryListView({ model: App.storyList });
        App.storyList.fetch();
        $('#stories-container').html(this.storyListView.render().el);

        this.postView = new PostStoryView();
        this.postView.render();
    }
});

var App = new AppRouter();

App.Location = {

    Get: function () {
        var location = {};
        _.extend(location, Backbone.Events);

        var success = function (geoLocation) {
            location.trigger('success', geoLocation.coords);
        };

        var failure = function () {
            location.trigger('failure');
        };

        navigator.geolocation.getCurrentPosition(success, failure);

        return location;
    }
};


var placeTypes = [
'accounting',
'airport',
'amusement_park',
'aquarium',
'art_gallery',
'atm',
'bakery',
'bank',
'bar',
'beauty_salon',
'bicycle_store',
'book_store',
'bowling_alley',
'bus_station',
'cafe',
'campground',
'car_dealer',
'car_rental',
'car_repair',
'car_wash',
'casino',
'cemetery',
'church',
'city_hall',
'clothing_store',
'convenience_store',
'courthouse',
'dentist',
'department_store',
'doctor',
'electrician',
'electronics_store',
'embassy',
'establishment',
'finance',
'fire_station',
'florist',
'food',
'funeral_home',
'furniture_store',
'gas_station',
'general_contractor',
'grocery_or_supermarket',
'gym',
'hair_care',
'hardware_store',
'health',
'hindu_temple',
'home_goods_store',
'hospital',
'insurance_agency',
'jewelry_store',
'laundry',
'lawyer',
'library',
'liquor_store',
'local_government_office',
'locksmith',
'lodging',
'meal_delivery',
'meal_takeaway',
'mosque',
'movie_rental',
'movie_theater',
'moving_company',
'museum',
'night_club',
'painter',
'park',
'parking',
'pet_store',
'pharmacy',
'physiotherapist',
'place_of_worship',
'plumber',
'police',
'post_office',
'real_estate_agency',
'restaurant',
'roofing_contractor',
'rv_park',
'school',
'shoe_store',
'shopping_mall',
'spa',
'stadium',
'storage',
'store',
'subway_station',
'synagogue',
'taxi_stand',
'train_station',
'travel_agency',
'university',
'veterinary_care',
'zoo'];


Backbone.history.start();
