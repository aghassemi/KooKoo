// Models
Story = Backbone.Model.extend({
    url: "../api/stories"
});

Stories = Backbone.Collection.extend({
    model: Story,
    url: "../api/stories"
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


// Router
var AppRouter = Backbone.Router.extend({

    routes: {
        "": "home",
        "post": "post"
    },

    home: function () {
        $('#nav-home').show();
        $('#nav-back').hide();
        $('#nav-main-right').show();
        $('#stories-container').show();
        $('#nav-post-right').hide();
        $('#post-story-container').hide();
        $('.toolbar-post').click(function (e) {
            App.navigate("post", {trigger: true});
        });
        App.storyList = new Stories();
        this.storyListView = new StoryListView({ model: App.storyList });
        App.storyList.fetch();
        $('#stories-container').html(this.storyListView.render().el);
    },

    post: function () {
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



Backbone.history.start();
