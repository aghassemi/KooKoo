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
        var me = this;
        this.postButton = $('#btn-story-post');
        this.stroyTextInput = $('#story-input');
        this.postButton.click(function () { me.post(); });
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
        App.SelectedPlace = null;
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


    uploadFile: function (storyId) {
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

        var saveStory = function (coords, imageUrl) {
            var placeId = null;
            var placeName = null;
            var placeLongitude = null;
            var placeLatitude = null
            if (App.SelectedPlace != null) {
                placeId = App.SelectedPlace.Id;
                placeName = App.SelectedPlace.attributes['name'];
                placeLongitude = App.SelectedPlace.attributes['geometry'].location.lng();
                placeLatitude = App.SelectedPlace.attributes['geometry'].location.lat();
            }
            var story = new Story();
            var latitude = null;
            var longitude = null;
            latitude = coords.latitude;
            longitude = coords.longitude;
            story.set(
                {
                    storytext: me.stroyTextInput.val(),
                    latitude: latitude,
                    longitude: longitude,
                    MoboziImageUrl: imageUrl,
                    PlaceId : placeId,
                    PlaceName: placeName,
                    placeLongitude : placeLongitude,
                    placeLatitude: placeLatitude
                }
            );
            me.thumbView.hide();
            story.save(
             {},
             {
                 success: function (model, response) {
                     App.navigate("", { trigger: true });
                     // if ($('#input-file').val()) {
                     //     me.uploadFile(model.get('Id'));
                     // } else {
                     
                     // }
                 }
             }
             );
            me.stroyTextInput.val('');
        };

        var loc = App.Location.Get()

        loc.on('success', function (coords) {
            if ($('#input-file').val()) {
                var file = document.getElementById('input-file').files[0];
                mobozi.image.uploadGetUrl({ "file": file }, function (response) {
                    saveStory(coords, response.data.imageUrl);
                });
            } else {
                saveStory(coords, null);
            }
            
        });

        loc.on('failure', function () {
            alert('Sorry! Only posted that allow location are allowed');
            App.navigate("", { trigger: true });
        });

        $('#btn-story-post').unbind('click');

    },

    render: function (eventName) {
        var me = this;
        $('#nav-home').hide();
        $('#nav-back').show();
        $('#nav-main-right').hide();
        $('#nav-post-right').show();
        $('#stories-container').hide();
        $('#post-story-container').show();
        this.stroyTextInput.focus();
        $('#places-container').html(this.placeListView.render().el);
        return this;
    }

});