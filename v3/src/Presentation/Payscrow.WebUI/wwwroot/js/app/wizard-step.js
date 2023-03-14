function Step(id, name, template, model) {
    var self = this;

    self.id = id;
    self.name = ko.observable(name);
    self.template = template;

    model.isDone = ko.observable(false);
    model.isPrestine = ko.observable(true);
    model.validation = ko.validatedObservable(model);
    model.hasError = ko.pureComputed(function () { return !model.validation.isValid() && !model.isPrestine(); });

    model.nextButtonTitle 

    self.model = ko.observable(model);

    self.getTemplate = function () {
        return self.template;
    }
}