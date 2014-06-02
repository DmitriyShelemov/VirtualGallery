
/* =============================================================
 * jquery.attachfile.js 
 * =============================================================
 * Requires:
 *     jquery.fileupload.js
 * ============================================================ */

(function ($) {
    var namespace = "attachfile",
        nsEvent = function() { return $.map(arguments, function(e) { return e + "." + namespace; }).join(' '); };

    var defaults = {
        uploadUrl: "/uploadUrl",
        maxFileSize: 20 * 1024 * 1024,
        fileTypeNotSupported: function () { },
        fileHasNoContent: function () { },
        fileTooBig: function () { },
        fail: function () { },
        onFileAddition: function () { },
        onFileClear: function() { }
    };

    var AttachFile = function (el, options) {
        this.settings = $.extend({}, defaults, options);
        this.$element = $(el);
        this.$nameTextBox = this.$element.find(".file-name");
        this.$uploadButton = this.$element.find(".btn-upload");
        this.$openFileButton = this.$element.find(".open-file");
        this.$clearFileButton = this.$element.find(".clear-file");
        this.fileuploadManager = null;
        this.fileToUpload = null;
        this.fileId = null;
        this.disabled = false;
        this.uploadSuccessCallback = null;
        this.init();
    };

    AttachFile.prototype = {
        constructor: AttachFile,

        init: function () {
            var self = this;
            this.fileId = this.$element.data('file-id') || null;

            this.$uploadButton.on(nsEvent('click', "mousedown", "mouseup"), function () {
                self.$uploadButton.tooltip("hide");
            });

            this._getInputFile().fileupload({
                url: this.settings.uploadUrl,
                dataType: 'json',
                singleFileUploads: true,
                add: $.proxy(this._fileAdded, this),
                done: $.proxy(this._fileUploaded, this)
            });
            
            this.$clearFileButton.on(nsEvent('click'), $.proxy(this.clear, this));
            this.$nameTextBox.parent().on(nsEvent('click'), $.proxy(this._fileNameClick, this));

            this._updateButtons();
        },
        
        destroy: function() {
            var ns = '.' + namespace;
            this._getInputFile().fileupload('destroy');
            this.$uploadButton.off(ns);
            this.$clearFileButton.off(ns);
            this.$nameTextBox.parent().off(ns);
        },
        
        upload: function (args) {
            var success = args[0];
            
            if (this.fileToUpload != null) {
                this.uploadSuccessCallback = success;
                this.fileToUpload.submit();
                this.fileToUpload = null;
                return;
            }

            if (typeof success === "function") success();
        },
        
        getFileId: function() {
            return this.fileId;
        },
        
        clear: function (e) {
            this._setFileId(null);
            this.fileToUpload = null;
            this._setFileName('');

            this._updateButtons();
            this.$element.trigger('change');

            if (typeof this.settings.onFileClear === "function")
                this.settings.onFileClear();
            
            if (e && e.type === 'click')
                e.preventDefault();
        },
        
        disable: function() {
            this.disabled = true;
            this._updateButtons();
        },

        enable: function() {
            this.disabled = false;
            this._updateButtons();
        },
        
        showValidationError: function () {            
            if (!this.$nameTextBox.hasClass("input-validation-error"))
                this.$nameTextBox.addClass("input-validation-error");
        },
        
        hideValidationError: function () {
            if (this.$nameTextBox.hasClass("input-validation-error"))
                this.$nameTextBox.removeClass("input-validation-error");
        },

        _fileNameClick: function (e, data) {
            if (this.$uploadButton.is(":visible"))
                this._getInputFile().trigger('click');

            if (this.$openFileButton.is(":visible"))
                window.location = this.$openFileButton.attr('href');            
        },

        _fileAdded: function (e, data) {
            var file = data.files[0];
            var fileName = file.name;

            if (typeof this.settings.acceptFileTypes !== "undefined" && typeof fileName !== "undefined" && !this.settings.acceptFileTypes.test(fileName)) {
                this.settings.fileTypeNotSupported(fileName);
                return;
            }

            if (typeof file.size !== "undefined" && file.size <= 0) {
            	this.settings.fileHasNoContent(fileName, file.size);
                return;
            }
	        
            if (typeof file.size !== "undefined" && file.size > this.settings.maxFileSize) {
            	this.settings.fileTooBig(fileName, file.size);
            	return;
            }

            this._setFileName(fileName);
            this.fileToUpload = data;

            this.hideValidationError();
            this._updateButtons();
            if (typeof this.settings.onFileAddition === "function")
                this.settings.onFileAddition();
            this.$element.trigger('change');
        },

        _fileUploaded: function(e, data) {
            if (data.result && typeof data.result.Success !== "undefined" && data.result.Success === false) {
            	this.settings.fail(data.result.ErrorMessage);
				this.clear();
                return;
            }

            this._setFileId(data.result.Data.UploadedFileId);
            this.$openFileButton.attr('href', data.result.Data.DownloadUrl);
            this._updateButtons();
            this.$element.trigger('change');
            
            if (typeof this.uploadSuccessCallback === "function") this.uploadSuccessCallback();
        },

        _setFileName: function (fileName) {
            this.$nameTextBox.val(fileName);
        },

        _setFileId: function (newFileId) {
            this.fileId = newFileId;
            this.$element.data('file-id', newFileId || '');
            this.$element.attr('data-file-id', newFileId || '');
        },

        _updateButtons: function () {
            if (this.fileId === null)
                this.$uploadButton.show();
            else
                this.$uploadButton.hide();

            if (this.fileId !== null)
                this.$openFileButton.show();
            else
                this.$openFileButton.hide();
            
            if (this.fileId !== null || this.fileToUpload !== null)
                this.$clearFileButton.show();
            else
                this.$clearFileButton.hide();

            var $controls = this.$uploadButton.add(this._getInputFile()).add(this.$openFileButton).add(this.$clearFileButton);
            if (this.disabled) {
                $controls.attr('disabled', 'disabled');
            } else {
                $controls.removeAttr("disabled");
            }
        },
        
        _getInputFile: function() {
            return this.$element.find("input[type='file']");
        }

    };

    $.fn.attachfile = function (option) {
        var data = $(this).data('attachfile');
        if (typeof option === 'string' && data && data[option]) {
            return typeof data[option] === "function" ? data[option](Array.prototype.slice.call(arguments, 1)) : data[option];
        } else if (typeof option === 'object' || !option) {
            return this.each(function () {
                var $this = $(this),
                    options = typeof option == 'object' && option;
                data = $this.data('attachfile');
                data || $this.data('attachfile', (data = new AttachFile(this, options)));
                if (typeof option == 'string') {
                    data[option]();
                }
            });
        } else {
            return $.error('Method with the name "' + option + '" does not exist for jQuery.attachfile');
        }
    };

    $.fn.attachfile.defaults = defaults;
    $.fn.attachfile.Constructor = AttachFile;

})(jQuery);

