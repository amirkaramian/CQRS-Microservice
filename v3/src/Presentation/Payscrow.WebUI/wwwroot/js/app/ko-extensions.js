ko.bindingHandlers.slideToggle = {
    init: function (element, valueAccessor) {
        var value = valueAccessor();
        $(element).toggle(ko.utils.unwrapObservable(value));
    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        ko.utils.unwrapObservable(value) ? $(element).slideDown() : $(element).slideUp();
    }
};

ko.bindingHandlers.number = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var defaults = ko.bindingHandlers.number.defaults,
            aba = allBindingsAccessor,
            unwrap = ko.utils.unwrapObservable,
            value = unwrap(valueAccessor()) || valueAccessor(),
            result = '',
            numarray;

        var separator = unwrap(aba().separator) || defaults.separator,
            decimal = unwrap(aba().decimal) || defaults.decimal,
            precision = unwrap(aba().precision) || defaults.precision,
            symbol = unwrap(aba().symbol) || defaults.symbol,
            after = unwrap(aba().after) || defaults.after;

        value = parseFloat(value) || 0;

        //if (precision > 0)
        value = value.toFixed(precision)

        numarray = value.toString().split('.');

        for (var i = 0; i < numarray.length; i++) {
            if (i == 0) {
                result += numarray[i].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1' + separator);
            } else {
                result += decimal + numarray[i];
            }
        }

        result = (after) ? result += symbol : symbol + ' ' + result;

        ko.bindingHandlers.text.update(element, function () { return result; });
    },
    defaults: {
        separator: ',',
        decimal: '.',
        precision: 0,
        symbol: '',
        after: false
    }
};

ko.bindingHandlers.tooltip = {
    init: function (element, valueAccessor) {
        var local = ko.utils.unwrapObservable(valueAccessor()),
            options = {};

        ko.utils.extend(options, ko.bindingHandlers.tooltip.options);
        ko.utils.extend(options, local);
        options.template = '<div class="tooltip tooltip-brand tooltop-auto-width" role="tooltip">\
                <div class="arrow"></div>\
                <div class="tooltip-inner"></div>\
            </div>';

        $(element).tooltip(options);

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).tooltip("dispose");
        });
    },
    options: {
        placement: "right",
        trigger: "click"
    }
    // usage: <input data-bind="value: name, tooltip: { title: help, trigger: 'hover', placement: 'top' }" />
};

ko.bindingHandlers.timeByHourMinute = {
    init: function (element, valueAccessor) {
        var time = moment(ko.utils.unwrapObservable(valueAccessor())).format('HH : mm');

        ko.utils.setTextContent(element, time);
    }
}

ko.extenders.numeric = function (target, precision) {
    var result = ko.computed({
        read: function () {
            if (!isNaN(parseFloat(target())) && isFinite(target())) {
                return parseFloat(target()).toFixed(precision).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            } else {
                return target();
            }
        },
        write: target
    });

    result.raw = target;
    return result;
};

ko.bindingHandlers.select2 = {
    after: ["options", "value"],
    init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        $(el).select2(ko.unwrap(valueAccessor()));
        ko.utils.domNodeDisposal.addDisposeCallback(el, function () {
            $(el).select2('destroy');
        });
    },
    update: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        var select2 = $(el).data("select2");
        if ("value" in allBindings) {
            var newValue = "" + ko.unwrap(allBindings.value);
            if ((allBindings.select2.multiple || el.multiple) && newValue.constructor !== Array) {
                select2.val([newValue.split(",")]);
            }
            else {
                select2.val([newValue]);
            }
        }
    }
};

ko.bindingHandlers.tooltipster = {
    init: function (element, valueAccessor) {
        $(element).tooltipster(ko.unwrap(valueAccessor()));
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var defaultOptions = {
            todayHighlight: true,
            format: 'dd/mm/yyyy',
            orientation: "bottom left",
            autoclose: true,
            templates: {
                leftArrow: '<i class="la la-angle-left"></i>',
                rightArrow: '<i class="la la-angle-right"></i>'
            }
        };

        var $elem = $(element);

        var options = $.extend(defaultOptions, allBindingsAccessor().datePickerOptions || {});
        $elem.datepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "changeDate", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                value(moment(event.date, 'LLLL').format('DD/MM/YYYY'));
            }
        });

        /* Handle disposal (if KO removes by the template binding) */
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $elem.datepicker("destroy");
        });
    },
    update: function (element, valueAccessor) {
        var widget = $(element).data("datepicker");
        //when the view model is updated, update the widget
        if (widget) {
            widget.date = ko.utils.unwrapObservable(valueAccessor());
            if (widget.date) {
                widget.setValue();
            }
        }
    }
};

ko.bindingHandlers.dateFormater = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        if (moment(value).isValid()) {
            var allBindings = allBindingsAccessor(),
                options = allBindings.dateOptions || {};

            if (options.locale) {
                ko.utils.setTextContent(element, moment(value).locale(options.locale).format('DD/MM/YYYY'));
            } else {
                ko.utils.setTextContent(element, moment(value).format('DD/MM/YYYY'));
            }
        }
    }
}

ko.bindingHandlers.widgetDateFormater = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        if (moment(value).isValid()) {
            var allBindings = allBindingsAccessor(),
                options = allBindings.dateOptions || {};

            ko.utils.setTextContent(element, moment(value).format('DD MMM, YY'));
        } else {
            ko.utils.setTextContent(element, "N/A");
        }
    }
}

ko.bindingHandlers.daterangePicker = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        $(element).daterangepicker({
            autoApply: true,
            startDate: moment().add(-30, 'days'), "endDate": moment(),
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            }
        });

        /**
         * On change event
         */
        ko.utils.registerEventHandler(element, "change", function () {
            var dateRangePicker = $(element).data('daterangepicker');
            var dateRange = valueAccessor();

            dateRange([dateRangePicker.startDate.format('YYYY-MM-DD'), dateRangePicker.endDate.format('YYYY-MM-DD')]);
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var dateRangePicker = $(element).data('daterangepicker');
        var dateRange = valueAccessor();

        dateRange([dateRangePicker.startDate.format('YYYY-MM-DD'), dateRangePicker.endDate.format('YYYY-MM-DD')]);
    }
};

ko.bindingHandlers.touchSpin = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var model = valueAccessor();

        var allBindings = allBindingsAccessor(),
            value = ko.utils.unwrapObservable(valueAccessor()),
            touchSpinOptions = allBindings.touchSpinOptions || {},
            defaults = ko.bindingHandlers.touchSpin.defaults,
            $element = $(element);

        var options = $.extend({ initval: value }, defaults, touchSpinOptions);

        $element.TouchSpin(options);
        model($element.val());

        $element.on('touchspin.on.startspin', function () {
            model($element.val());
        });

        //ko.bindingHandlers.touchSpin.updateEnableState(element, allBindings);
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var allBindings = allBindingsAccessor();

        ko.bindingHandlers.value.update(element, valueAccessor);
        //ko.bindingHandlers.touchSpin.updateEnableState(element, allBindings);
    },
    updateEnableState: function (element, allBindings) {
        var $element = $(element),
            $plus = $element.parent().find(".bootstrap-touchspin-up"),
            $minus = $element.parent().find(".bootstrap-touchspin-down");

        if (allBindings.enable !== undefined) {
            if (ko.utils.unwrapObservable(allBindings.enable) === true) {
                $plus.removeAttr('disabled');
                $minus.removeAttr('disabled');
                // $element.trigger("touchspin.updatesettings", { mousewheel: true });
            } else {
                $plus.attr('disabled', 'disabled');
                $minus.attr('disabled', 'disabled');
                // $element.trigger("touchspin.updatesettings", { mousewheel: false });
            }
        }

        if (allBindings.disable !== undefined) {
            if (ko.utils.unwrapObservable(allBindings.disable) === false) {
                $plus.removeAttr('disabled');
                $minus.removeAttr('disabled');
                // $element.trigger("touchspin.updatesettings", { mousewheel: true });
            } else {
                $plus.attr('disabled', 'disabled');
                $minus.attr('disabled', 'disabled');
                // $element.trigger("touchspin.updatesettings", { mousewheel: false });
            }
        }
    },
    defaults: {
        min: 0,
        max: 100,
        initval: 1,
        step: 1,
        decimals: 0,
        stepinterval: 100,
        forcestepdivisibility: 'round',  // none | floor | round | ceil
        stepintervaldelay: 500,
        prefix: "",
        postfix: "",
        prefix_extraclass: "",
        postfix_extraclass: "",
        booster: true,
        boostat: 10,
        maxboostedstep: false,
        mousewheel: false, // Aguarda fixar ou incluir o disabled corretamente
        buttondown_class: "btn btn-default",
        buttonup_class: "btn btn-default"
    }
};

ko.bindingHandlers.dropzone = {
    init: function (element, valueAccessor) {
        var value = ko.unwrap(valueAccessor());

        var options = {
            maxFileSize: 15,
            removeFileOnSuccess: true
            //createImageThumbnails: false,
        };

        $.extend(options, value);

        $(element).addClass('dropzone');
        var dropZone = new Dropzone(element, options); // jshint ignore:line
        dropZone.on('success', function (file) {
            if (options.removeFileOnSuccess) {
                dropZone.removeFile(file);
            }
        });
    }
};

ko.bindingHandlers.trimLengthText = {};
ko.bindingHandlers.trimText = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var trimmedText = ko.computed(function () {
            var untrimmedText = ko.utils.unwrapObservable(valueAccessor());
            var defaultMaxLength = 20;
            var minLength = 5;
            var maxLength = ko.utils.unwrapObservable(allBindingsAccessor().trimTextLength) || defaultMaxLength;
            if (maxLength < minLength) maxLength = minLength;

            if (untrimmedText != null) {
                var text = untrimmedText.length > maxLength
                    ? untrimmedText.substring(0, maxLength - 1) + '...'
                    : untrimmedText;
                return text;
            } else {
                return '';
            }
        });
        ko.applyBindingsToNode(element, {
            text: trimmedText
        }, viewModel);

        return {
            controlsDescendantBindings: true
        };
    }
};

ko.bindingHandlers.valueNumber = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        /**
         * Adapted from the KO hasfocus handleElementFocusChange function
         */
        function elementIsFocused() {
            var isFocused = false,
                ownerDoc = element.ownerDocument;
            if ("activeElement" in ownerDoc) {
                var active;
                try {
                    active = ownerDoc.activeElement;
                } catch (e) {
                    // IE9 throws if you access activeElement during page load
                    active = ownerDoc.body;
                }
                isFocused = (active === element);
            }

            return isFocused;
        }

        /**
         * Adapted from the KO hasfocus handleElementFocusChange function
         *
         * @param {boolean} isFocused whether the current element has focus
         */
        function handleElementFocusChange(isFocused) {
            elementHasFocus(isFocused);
        }

        var observable = valueAccessor(),
            properties = allBindingsAccessor(),
            elementHasFocus = ko.observable(elementIsFocused()),
            handleElementFocusIn = handleElementFocusChange.bind(null, true),
            handleElementFocusOut = handleElementFocusChange.bind(null, false);

        var interceptor = ko.computed({
            read: function () {
                var currentValue = ko.utils.unwrapObservable(observable);
                if (elementHasFocus()) {
                    return (!isNaN(currentValue) && (currentValue !== null) && (currentValue !== undefined))
                        ? currentValue.toString().replace(".", Globalize.findClosestCulture().numberFormat["."]) // Displays correct decimal separator for the current culture (so de-DE would format 1.234 as "1,234")
                        : null;
                } else {
                    var format = properties.numberFormat || "n2",
                        formattedNumber = Globalize.numberFormatter(currentValue, format);

                    return formattedNumber;
                }
            },
            write: function (newValue) {
                var currentValue = ko.utils.unwrapObservable(observable),
                    numberValue = Globalize.parseFloat(newValue);

                if (!isNaN(numberValue)) {
                    if (numberValue !== currentValue) {
                        // The value has changed so update the observable
                        observable(numberValue);
                    }
                } else if (newValue.length === 0) {
                    if (properties.isNullable) {
                        // If newValue is a blank string and the isNullable property has been set then nullify the observable
                        observable(null);
                    } else {
                        // If newValue is a blank string and the isNullable property has not been set then set the observable to 0
                        observable(0);
                    }
                }
            }
        });

        ko.utils.registerEventHandler(element, "focus", handleElementFocusIn);
        ko.utils.registerEventHandler(element, "focusin", handleElementFocusIn); // For IE
        ko.utils.registerEventHandler(element, "blur", handleElementFocusOut);
        ko.utils.registerEventHandler(element, "focusout", handleElementFocusOut); // For IE

        if (element.tagName.toLowerCase() === 'input') {
            ko.applyBindingsToNode(element, { value: interceptor });
        } else {
            ko.applyBindingsToNode(element, { text: interceptor });
        }
    }
};

ko.validation.rules["atLeastOne"] = {
    validator: function (value, validate) {
        if (validate && Array.isArray(value)) {
            return !!value.length;
        }
        return true;
    },
    message: "Please add at least one."
};

ko.validation.rules['isLargerThan'] = {
    validator: function (val, otherVal) {
        /*important to use otherValue(), because ko.computed returns a function,
        and the value is inside that function*/
        otherVal = otherVal();
        return parseFloat(val) > parseFloat(otherVal);
    },
    message: 'This need to be larger than the First Number '
};

ko.validation.rules['isLessThan'] = {
    validator: function (val, otherVal) {
        //otherVal = otherVal();
        return parseFloat(val) < parseFloat(otherVal);
    },
    message: 'This need to be larger than the First Number '
};

ko.validation.rules['isLessThanOrEqual'] = {
    validator: function (val, otherVal) {
        //otherVal = otherVal();
        return parseFloat(val) <= parseFloat(otherVal);
    },
    message: 'This need to be larger than or equal the First Number '
};

ko.validation.rules['areSame'] = {
    getValue: function (o) {
        return (typeof o === 'function' ? o() : o);
    },
    validator: function (val, otherField) {
        return val === this.getValue(otherField);
    },
    message: 'The fields must have the same value'
};

ko.validation.rules['mustNotEqual'] = {
    validator: function (val, otherVal) {
        return val !== otherVal;
    },
    message: 'The field must equal {0}'
};