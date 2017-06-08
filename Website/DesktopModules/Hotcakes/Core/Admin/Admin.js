﻿// JScript File

//To prevent "Uncaught TypeError: Cannot read property 'analytics' of undefined" in older DNN versions
var dnn = dnn || {};

(function ($) {

    hcc.config.dialogClass = "hcFormPopup";

    // Global functions
    hcConfirm = function (event, msg, callback) {
        var $ok = $("<a href='#' class='hcPrimaryAction'>OK</a>");
        var $cancel = $("<a href='#' class='hcSecondaryAction'>Cancel</a>");
        var $dlg = $("<div class='hcConfirmDialog'><p><span class='msg'></span></p><div class='hcActionsRight'><ul class='hcActions'><li><a class='ok'></a></li><li><a class='cancel'></a></li></ul></div></div>");
        $dlg.find(".msg").replaceWith(msg);
        $dlg.find(".ok").replaceWith($ok);
        $dlg.find(".cancel").replaceWith($cancel);

        var defAction = $(event.target).attr('href');
        if (!callback && defAction) {
            callback = function () { $dlg.dialog('close'); window.location.href = defAction; };
        }

        $ok.click({ param1: $dlg }, callback);
        $cancel.click(function () { $dlg.dialog('close') });

        $dlg.hcDialog({ title: "Confirm", width: 400, maxHeight: 300 });
        $ok.focus();
        return false;
    };
    hcAlert = function (event, msg) {
        var $ok = $("<a href='#' class='hcPrimaryAction'>OK</a>");
        var $dlg = $("<div><p><span class='msg'></span></p><div class='hcActionsRight'><ul class='hcActions'><li><a class='ok'></a></li></ul></div></div>");
        $dlg.find(".msg").replaceWith(msg);
        $dlg.find(".ok").replaceWith($ok);

        $ok.click(function () { $dlg.dialog('close') });
        $dlg.hcDialog({ title: "Alert", width: 400, maxHeight: 300 });
        $ok.focus();
        return false;
    };

    hcAttachUpdatePanelLoader = function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function (s, a) {

            if ($(".ui-dialog").length == 0 &&
                a &&
                a._updatePanelsToUpdate &&
                a._updatePanelsToUpdate.length > 0) {

                var panelId = s._updatePanelClientIDs[$.inArray(a._updatePanelsToUpdate[0], s._updatePanelIDs)];
                $("#" + panelId).ajaxLoader("start");
            }

            $(".ui-dialog-content").ajaxLoader("start");
        });
        prm.add_pageLoaded(function (s, a) {
            $(a._panelsUpdated).ajaxLoader("stop");
            $(".ui-dialog-content").ajaxLoader("stop");
        });
    };

    hcUpdatePanelReady = function (func) {
        func();

        if (typeof Sys != 'undefined')
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(func);
    };

    String.prototype.trim = function () {
        return this.replace(/^\s*/, "").replace(/\s*$/, "");
    }

    String.prototype.padL = function (nLength, sChar) {
        var sreturn = this;
        while (sreturn.length < nLength) {
            sreturn = String(sChar) + sreturn;
        }
        return sreturn;

    }

    $.fn.hcImageUpload = function (settings) {
        var $divImageBox = settings.divImage;
        var $inputTempPath = settings.inputTempFilePath;
        var $inputFileName = settings.inputOriginalFileName;
        $(this).find("input[type='file']").fileupload({
            dataType: 'json',
            done: function (e, data) {
                if (data.result.TempFileName) {
                    var imgUrl = data.result.TempFileName;
                    var $img = $("<img>").attr("src", imgUrl);
                    $divImageBox.find('span').hide();
                    $divImageBox.find('img').hide();
                    $divImageBox.append($img);

                    $inputFileName.val(data.result.FileName);
                    $inputTempPath.val(data.result.TempFileName);
                } else {
                    $divImageBox.find('img').hide();
                    $divImageBox.find('span').text(data.result.Message).attr("class", "hcFormError");
                }
            }
        });

        var $clearBtn = this.find(".hcIconClose");
        this.hover(
            function () {
                $clearBtn.show();
            },
            function () {
                $clearBtn.hide();
            });
        $clearBtn.click(function () {
            $divImageBox.find('img').hide();
            $inputFileName.val('');
            $inputTempPath.val('REMOVED');
        });
    };
})(jQuery);

jQuery(function ($) {
    $(".hcOpenPopup").click(function () {
        var myModal = $(this).attr('href');
        $(myModal).hcDialog();
        return false;
    });

    $(".hcIconInfo, .hcTextInfo").hover(function () {
        $(this).find(".hcFormInfo").show();
    },
    function () {
        $(this).find(".hcFormInfo").hide();
    });
});


function createAddressValidationInputs() {
    var AddressValidationInputs = {
        init: function (form, msg, ddlCountry, txtFirstName, txtLastName, txtCompany, txtAddressLine1, txtAddressLine2, txtCity, ddlRegion, txtPostalCode, txtPhone) {
            this.$form = $(form);
            this.$msg = $(msg);
            this.$msg.hide();
            this.$fields = this.$form.find("input, select");

            if (ddlCountry && $(ddlCountry).length > 0) {
                this.$ddlCountry = $(ddlCountry);
            }
            if (txtFirstName && $(txtFirstName).length > 0) {
                this.$txtFirstName = $(txtFirstName);
            }
            if (txtLastName && $(txtLastName).length > 0) {
                this.$txtLastName = $(txtLastName);
            }
            if (txtCompany && $(txtCompany).length > 0) {
                this.$txtCompany = $(txtCompany);
            }
            if (txtAddressLine1 && $(txtAddressLine1).length > 0) {
                this.$txtAddressLine1 = $(txtAddressLine1);
            }
            if (txtAddressLine2 && $(txtAddressLine2).length > 0) {
                this.$txtAddressLine2 = $(txtAddressLine2);
            }
            if (txtCity && $(txtCity).length > 0) {
                this.$txtCity = $(txtCity);
            }
            if (ddlRegion && $(ddlRegion).length > 0) {
                this.$ddlRegion = $(ddlRegion);
            }
            if (txtPostalCode && $(txtPostalCode).length > 0) {
                this.$txtPostalCode = $(txtPostalCode);
            }
            if (txtPhone && $(txtPhone).length > 0) {
                this.$txtPhone = $(txtPhone);
            }

            this.normalizedAddress = null;
            this.$form.data('address-validation-inputs', this);
        },
        getAddressData: function () {
            return {
                CountryBvin: this.getCountry(),
                FirstName: this.getFirstName(),
                LastName: this.getLastName(),
                Company: this.getCompany(),
                Line1: this.getAddressLine1(),
                Line2: this.getAddressLine2(),
                City: this.getCity(),
                RegionBvin: this.getRegion(),
                PostalCode: this.getPostalCode(),
                Phone: this.getPhone()
            };
        },
        setNormalized: function () {
            if (this.normalizedAddress) {
                if (this.normalizedAddress.Line1 == "") {
                    this.setAddressLine1(this.normalizedAddress.Line2);
                    this.setAddressLine2("");
                } else {
                    this.setAddressLine1(this.normalizedAddress.Line1);
                    this.setAddressLine2(this.normalizedAddress.Line2);
                }
                this.setCity(this.normalizedAddress.City);
                this.setPostalCode(this.normalizedAddress.PostalCode);
                this.setRegion(this.normalizedAddress.RegionBvin);
            }
        },
        getCountry: function () {
            return this.$ddlCountry ? this.$ddlCountry.val() : null;
        },
        setCountry: function (value) {
            if (this.$ddlCountry) this.$ddlCountry.val(value);
        },
        getFirstName: function () {
            return this.$txtFirstName ? this.$txtFirstName.val() : null;
        },
        setFirstName: function (value) {
            if (this.$txtFirstName) this.$txtFirstName.val(value);
        },
        getLastName: function () {
            return this.$txtLastName ? this.$txtLastName.val() : null;
        },
        setLastName: function (value) {
            if (this.$txtLastName) this.$txtLastName.val(value);
        },
        getCompany: function () {
            return this.$txtCompany ? this.$txtCompany.val() : null;
        },
        setCompany: function (value) {
            if (this.$txtCompany) this.$txtCompany.val(value);
        },
        getAddressLine1: function () {
            return this.$txtAddressLine1 ? this.$txtAddressLine1.val() : null;
        },
        setAddressLine1: function (value) {
            if (this.$txtAddressLine1) this.$txtAddressLine1.val(value);
        },
        getAddressLine2: function () {
            return this.$txtAddressLine2 ? this.$txtAddressLine2.val() : null;
        },
        setAddressLine2: function (value) {
            if (this.$txtAddressLine2) this.$txtAddressLine2.val(value);
        },
        getCity: function () {
            return this.$txtCity ? this.$txtCity.val() : null;
        },
        setCity: function (value) {
            if (this.$txtCity) this.$txtCity.val(value);
        },
        getRegion: function () {
            return this.$ddlRegion ? this.$ddlRegion.val() : null;
        },
        setRegion: function (value) {
            if (this.$ddlRegion) this.$ddlRegion.val(value);
        },
        getPostalCode: function () {
            return this.$txtPostalCode ? this.$txtPostalCode.val() : null;
        },
        setPostalCode: function (value) {
            if (this.$txtPostalCode) this.$txtPostalCode.val(value);
        },
        getPhone: function () {
            return this.$txtPhone ? this.$txtPhone.val() : null;
        },
        setPhone: function (value) {
            if (this.$txtPhone) this.$txtPhone.val(value);
        }
    };

    return AddressValidationInputs;
}

function createAddressValidator() {
    var AddressValidator = {
        init: function (primaryInputs, btnSave, secondaryInputs, chkUseSameAddress) {
            this.primaryInputs = primaryInputs;
            this.hasSecondary = secondaryInputs != undefined;
            if (this.hasSecondary) {
                this.secondaryInputs = secondaryInputs;
                if (chkUseSameAddress) {
                    this.$chkUseSameAddress = $(chkUseSameAddress);
                }
            }
            this.$btnSave = $(btnSave);
            this.$dialog = $("#hcNormalizedAddressDlg");
            this.$dialogBtnSave = this.$dialog.find(".hcSaveNormalizedAction");
            this.$dialogPrimaryBlock = this.$dialog.find(".hcPrimaryBlock");
            this.$dialogPrimaryNormalized = this.$dialogPrimaryBlock.find(".hcNormalizedAddress");
            this.$dialogPrimaryOriginal = this.$dialogPrimaryBlock.find(".hcOriginalAddress");
            this.$dialogPrimaryRadio = this.$dialogPrimaryBlock.find("[name='primary']");
            this.$dialogSecondaryBlock = this.$dialog.find(".hcSecondaryBlock");
            this.$dialogSecondaryNormalized = this.$dialogSecondaryBlock.find(".hcNormalizedAddress");
            this.$dialogSecondaryOriginal = this.$dialogSecondaryBlock.find(".hcOriginalAddress");
            this.$dialogSecondaryRadio = this.$dialogSecondaryBlock.find("[name='secondary']");

            this.preventSubmit = true;
            this.bindEvents();
        },
        bindEvents: function () {
            this.primaryInputs.$fields.change(function (e) { AddressValidator.validateAddress(e); });
            if (this.hasSecondary) {
                this.secondaryInputs.$fields.change(function (e) { AddressValidator.validateAddress(e); });
            }
            this.validateAddress();
            this.$btnSave.click(function (e) { AddressValidator.save(e); });
            this.$dialogBtnSave.click(function (e) { AddressValidator.saveNormalized(e); });
        },
        checkSecondary: function () {
            return this.hasSecondary && !(this.$chkUseSameAddress && this.$chkUseSameAddress.is(':checked'));
        },
        getAddressData: function () {
            var primaryData = this.primaryInputs.getAddressData();
            var data = {
                CountryBvin: primaryData.CountryBvin,
                FirstName: primaryData.FirstName,
                LastName: primaryData.LastName,
                Company: primaryData.Company,
                Line1: primaryData.Line1,
                Line2: primaryData.Line2,
                City: primaryData.City,
                RegionBvin: primaryData.RegionBvin,
                PostalCode: primaryData.PostalCode,
                Phone: primaryData.Phone
            };

            if (this.checkSecondary()) {
                var secondaryData = this.secondaryInputs.getAddressData();
                data.SecCountryBvin = secondaryData.CountryBvin;
                data.SecFirstName = secondaryData.FirstName;
                data.SecLastName = secondaryData.LastName;
                data.SecCompany = secondaryData.Company;
                data.SecLine1 = secondaryData.Line1;
                data.SecLine2 = secondaryData.Line2;
                data.SecCity = secondaryData.City;
                data.SecRegionBvin = secondaryData.RegionBvin;
                data.SecPostalCode = secondaryData.PostalCode;
                data.SecPhone = secondaryData.Phone;
            }

            return data;
        },
        validateAddress: function (e, callback) {
            var self = this;
            var data = this.getAddressData();
            if (this.checkSecondary()) {
                data.ContainsSecondary = true;
            }

            var primaryHasEmpty = (data.Line1 == "" && data.Line2 == "") || data.City == "" || data.PostalCode == "" || data.RegionBvin == "";
            var secondaryHasEmpty = this.checkSecondary() && ((data.SecLine1 == "" && data.SecLine2 == "") || data.SecCity == "" || data.SecPostalCode == "" || data.SecRegionBvin == "");

            if (primaryHasEmpty && (!this.checkSecondary() || secondaryHasEmpty)) {
                this.primaryInputs.$msg.hide();
                if (this.checkSecondary()) {
                    this.secondaryInputs.$msg.hide();
                }
                if (callback) callback(null);
            } else {
                data.Method = "VerifyAddress";
                $.post(hcc.getResourceUrl('Admin/Configuration/ConfigHandler.ashx'), data, null, "json")
                    .done(function (result) {
                        self.handleValidation(data, result);
                        if (callback) callback(result);
                    })
                    .fail(function () {
                        if (console && console.log)
                            console.log("ConfigHandler.ashx failed");
                    });
            }
        },
        handleValidation: function (postData, result) {
            if ((postData.Line1 != "" || postData.Line2 != "") && postData.City != "" && postData.PostalCode != "" && postData.RegionBvin != ""
                && result.Message != null && result.Message != "") {
                this.primaryInputs.$msg.html(result.Message);
                this.primaryInputs.$msg.show();
            } else {
                this.primaryInputs.$msg.hide();
            }
            if (this.checkSecondary()) {
                if ((postData.SecLine1 != "" || postData.SecLine2 != "") && postData.SecCity != "" && postData.SecPostalCode != "" && postData.SecRegionBvin != ""
                    && result.SecMessage != null && result.SecMessage != "") {
                    this.secondaryInputs.$msg.html(result.SecMessage);
                    this.secondaryInputs.$msg.show();
                } else {
                    this.secondaryInputs.$msg.hide();
                }
            }
        },
        showDialog: function (res) {
            if (res.NormalizedAddress != null) {
                this.primaryInputs.normalizedAddress = res.NormalizedAddress;
                this.$dialogPrimaryNormalized.html(res.NormalizedAddressHtml);
                this.$dialogPrimaryOriginal.html(res.OriginalAddressHtml);
                this.$dialogPrimaryBlock.show();
            } else {
                this.$dialogPrimaryBlock.hide();
                this.primaryInputs.normalizedAddress = null;
            }
            if (this.checkSecondary() && res.SecNormalizedAddress != null) {
                this.secondaryInputs.normalizedAddress = res.SecNormalizedAddress;
                this.$dialogSecondaryNormalized.html(res.SecNormalizedAddressHtml);
                this.$dialogSecondaryOriginal.html(res.SecOriginalAddressHtml);
                this.$dialogSecondaryBlock.show();
            } else {
                if (this.secondaryInputs != undefined) {
                    this.secondaryInputs.normalizedAddress = null;
                }
                this.$dialogSecondaryBlock.hide();
            }
            this.$dialog.hcDialog({ height: 'auto' });
        },
        save: function (e) {
            if (this.preventSubmit) {
                var self = this;
                e.stopPropagation();
                e.preventDefault();
                this.primaryInputs.$form.ajaxLoader("start");
                if (this.checkSecondary()) {
                    this.secondaryInputs.$form.ajaxLoader("start");
                }
                this.validateAddress(e, function (res) {
                    self.primaryInputs.$form.ajaxLoader("stop");
                    if (self.checkSecondary()) {
                        self.secondaryInputs.$form.ajaxLoader("stop");
                    }
                    if (res != null && (res.IsValid || res.SecIsValid) && (res.NormalizedAddress != null || res.SecNormalizedAddress != null)) {
                        self.showDialog(res);
                    } else {
                        self.saveForm();
                    }
                });
                return false;
            }
        },
        saveForm: function () {
            this.preventSubmit = false;
            if (this.$btnSave.is('a')) {
                window.location.href = this.$btnSave.attr("href");
            } else {
                this.$btnSave.click();
            }
        },
        saveNormalized: function (e) {
            if (this.primaryInputs.normalizedAddress != null && this.$dialogPrimaryRadio.filter(":checked").val() == "N") {
                this.primaryInputs.setNormalized();
            }
            if (this.checkSecondary() && this.secondaryInputs.normalizedAddress != null && this.$dialogSecondaryRadio.filter(":checked").val() == "N") {
                this.secondaryInputs.setNormalized();
            }
            this.$dialog.dialog('close');
            this.saveForm();
            return false;
        }
    };

    return AddressValidator;
}
