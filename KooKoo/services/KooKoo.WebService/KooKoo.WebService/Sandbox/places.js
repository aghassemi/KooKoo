/*places */

Place = Backbone.Model.extend({
});

Places = Backbone.Collection.extend({
    fetch: function (options) {

        var me = this;

        var googlePlaceCallback = function (results, status) {
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

    events: { 'click': 'select' },

    select: function () {
        $('.place').removeClass('selected');
        $('.place', this.el).addClass('selected');
        App.SelectedPlace = this.model;
    },

    render: function (eventName) {
        $(this.el).html(this.template(this.model.toJSON()));
        return this;
    }

});


var placeTypes = [
'airport',
'amusement_park',
'aquarium',
'art_gallery',
'atm',
'bakery',
'bank',
'bar',
'beauty_salon',
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
'courthouse',
'dentist',
'department_store',
'doctor',
'electronics_store',
'embassy',
'establishment',
'finance',
'florist',
'food',
'funeral_home',
'furniture_store',
'gas_station',
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
'police',
'post_office',
'restaurant',
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
