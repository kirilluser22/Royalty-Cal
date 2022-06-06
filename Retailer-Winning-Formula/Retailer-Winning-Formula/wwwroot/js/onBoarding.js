let jsonObj = {};
//Contact Information start
$(".contactInfo-saveBtn").click(function () {
    if (!validateAllContactInfoFields()) {
        return false;
    }

    $(".switch1").css("display", "none");
    $(".switch2").fadeIn(300);
    $('.switch__list ul li').removeClass("current__switch");
    $(".switch__list ul li:nth-child(2)").addClass("current__switch");

    jsonObj["ContactInfo"] = createContactInfoJson();
    console.log(jsonObj)
})
//Contact Information end

//validations- contactInfo businessInfo locationInfo
$(document).ready(function () {
    $("#frmLocationInfo").validate({
        errorPlacement: function (error, element) {
            if (element.hasClass("locationPhoneNo")) {
                error.insertAfter(".locationPhoneNoDropDown-" + element.attr("data-phoneDropDown"));
            }
            else {
                error.insertAfter(element);
            }
        },
        onfocusout: function (element) {
            $(element).valid();
        }
    });
    $('.locationName').each(function () {
        $(this).rules("add", {
            required: true,
            messages: {
                required: "Location Name is required"
            }
        });
    });
    $('.locationAddress').each(function () {
        $(this).rules("add", {
            required: true,
            messages: {
                required: "Address is required"
            }
        });
    });
    $('.locationCity').each(function () {
        $(this).rules("add", {
            required: true,
            messages: {
                required: "City is required"
            }
        });
    });
    $('.locationProvince').each(function () {
        $(this).rules("add", {
            required: true,
            messages: {
                required: "Province is required"
            }
        });
    });
    $('.locationPostalCode').each(function () {
        $(this).rules("add", {
            required: true,
            messages: {
                required: "Postal Code is required"
            }
        });
    });
    $('.locationCountry').each(function () {
        $(this).rules("add", {
            required: true,
            messages: {
                required: "Country is required"
            }
        });
    });
    $('.locationEmail').each(function () {
        $(this).rules("add", {
            required: true,
            email: true,
            messages: {
                required: "Email is required"
            }
        });
    });
    $('.locationPhoneNo').each(function () {
        $(this).rules("add", {
            required: true,
            maxlength: 10,
            minlength: 10,
            digits: true,
            messages: {
                required: "Phone Number is required"
            }
        });
    });
    //end

    //businessInfo
    $("#frmBusinessInfo").validate({
        rules: {
            StoreName: "required",
            BusinessLegalName: "required"
        },
        messages: {
            StoreName: "Store Name is required",
            BusinessLegalName: "Business Legal Name is required"
        },
        onfocusout: function (element) {
            $(element).valid();
        },
    });
    //end

    //contactInfo
    $("#frmContactInfo").validate({
        rules: {
            FirstName: "required",
            LastName: "required",
            Email: {
                required: true,
                email: true
            },
            PhoneNumber: {
                required: true,
                maxlength: 10,
                minlength: 10,
                digits: true
            }
        },
        messages: {
            FirstName: "First Name is required",
            LastName: "Last Name is required",
            Email: "Email is required",
            PhoneNumber: "Phone Number is required"
        },
        onfocusout: function (element) {
            $(element).valid();
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "PhoneNumber") {
                error.insertAfter(".contactPhoneDropDown");
            }
            else {
                error.insertAfter(element);
            }
        }
    });
});
//BusinessInfo start
$(".businessInfo-saveBtn").click(function () {
    let isValid = validateAllBusinessInfoFields();
    console.log(isValid)

    if (!isValid)
        return isValid;
    $(".switch2").css("display", "none");
    $(".switch3").fadeIn(300);
    $('.switch__list ul li').removeClass("current__switch");
    $(".switch__list ul li:nth-child(3)").addClass("current__switch");

    jsonObj["BusinessInfo"] = createBusinessInfoJson();
    console.log(jsonObj)
});
//BusinessInfo end


//LocaitonInfo start

$(".locationInfoRow-AddBtn").click(function () {
    //$("#msgNotifierBody").html("Please Wait");
    //$("#msgModal").modal();
    $(".addLocationBtnSection").addClass("d-none");
    $(".loader1").removeClass("d-none");
    let rowNo = $('.locationRow').length;
    $.ajax({
        type: 'GET',
        data: { row: rowNo },
        url: '/SmartPartnerOnBoarding/LocationInfoTemplete',
        success: function (result) {
            $('.locationRow').last().after(result);
            pasteToLocationName();
            $(".addLocationBtnSection").removeClass("d-none");
            $(".loader1").addClass("d-none");
            //$("#msgModal").modal("hide");
            $('.locationName').each(function () {
                $(this).rules("add", {
                    required: true,
                    messages: {
                        required: "Location Name is required"
                    }
                });
            });
            $('.locationAddress').each(function () {
                $(this).rules("add", {
                    required: true,
                    messages: {
                        required: "Address is required"
                    }
                });
            });
            $('.locationCity').each(function () {
                $(this).rules("add", {
                    required: true,
                    messages: {
                        required: "City is required"
                    }
                });
            });
            $('.locationProvince').each(function () {
                $(this).rules("add", {
                    required: true,
                    messages: {
                        required: "Province is required"
                    }
                });
            });
            $('.locationPostalCode').each(function () {
                $(this).rules("add", {
                    required: true,
                    messages: {
                        required: "Postal Code is required"
                    }
                });
            });
            $('.locationCountry').each(function () {
                $(this).rules("add", {
                    required: true,
                    messages: {
                        required: "Country is required"
                    }
                });
            });
            $('.locationEmail').each(function () {
                $(this).rules("add", {
                    required: true,
                    email: true,
                    messages: {
                        required: "Email is required"
                    }
                });
            });
            $('.locationPhoneNo').each(function () {
                $(this).rules("add", {
                    required: true,
                    maxlength: 10,
                    minlength: 10,
                    digits: true,
                    messages: {
                        required: "Phone Number is required"
                    }
                });
            });

            // $("#frmLocationInfo").validate();
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
    return false;
});
$(".locationInfo-saveBtn").click(function () {
    let isValid = $("#frmLocationInfo").valid();
    if (!isValid)
        return false;
    let rowNo = $('.locationRow').length;
    jsonObj["ContactInfo"] = createContactInfoJson();
    jsonObj["BusinessInfo"] = createBusinessInfoJson();
    jsonObj["Locations"] = createLocationInfoJson();    
    console.log(jsonObj);
    $(".info__container").addClass("d-none");

    //contactInfoReview
    let nameHtml = "<div class='el__column'><h6>Name</h6><p>" + jsonObj["ContactInfo"].FirstName + " " + jsonObj["ContactInfo"].LastName + "</p></div>";
    let emailHtml = "<div class='el__column'><h6>Email</h6><p>" + jsonObj["ContactInfo"].Email + "</p></div>";
    let phoneNumberHtml = "<div class='el__column'><h6>Phome number</h6><p>" + jsonObj["ContactInfo"].PhoneNumber + "</p></div>";
    //TODO: make Timezone,Currency,Language picture dynamic
    //let currencyHtml = "<div class='el__column'><h6>Currency</h6><p><span><img src='/img/newImgs/langicon.svg' alt='langicon'></span>" + jsonObj["ContactInfo"].Currency + "</p></div>";
    let languageHtml = "<div class='el__column'><h6>Language</h6><p><span><img src='/img/newImgs/langicon.svg' alt='langicon'></span>" + jsonObj["ContactInfo"].Language + "</p></div>";
    $("#contactInfo-IstHalf").html(nameHtml + emailHtml + phoneNumberHtml);
    $("#contactInfo-2ndHalf").html(languageHtml);

    //businessInfoReview
    let storeNameHtml = "<div class='el__column'><h6>Store Name</h6><p>" + jsonObj["BusinessInfo"].StoreName + "</p></div>";
    let businessLegalNameHtml = "<div class='el__column'><h6>Business Legal Name</h6><p>" + jsonObj["BusinessInfo"].BusinessLegalName + "</p></div>";
    let productAndServicesHtml = "<div class='el__column'><h6>Products and Services</h6><p>" + jsonObj["BusinessInfo"].ProductAndServices + "</p></div>";
    let buyingGroupHtml = "<div class='el__column'><h6>Buying group</h6><p>" + jsonObj["BusinessInfo"].BuyingGroup + "</p></div>";
    //let businessTradeNameHtml = "<div class='el__column'><h6>Business Trade Name</h6><p>" + jsonObj["BusinessInfo"].BusinessTradeName + "</p></div>";

    let partnerIdHtml = "<div class='el__column'><h6>Member ID#</h6><p>" + jsonObj["BusinessInfo"].PartnerId + "</p></div>";
    let businessInfoCurrencyHtml = "<div class='el__column'><h6>Currency</h6><p><span><img src='/img/newImgs/langicon.svg' alt='langicon'></span>" + jsonObj["BusinessInfo"].Currency + "</p></div>";
    $("#businessInfo-IstHalf").html(storeNameHtml + businessLegalNameHtml + productAndServicesHtml + buyingGroupHtml);
    $("#businessInfo-2ndHalf").html(partnerIdHtml + businessInfoCurrencyHtml);
    //locationInfoReview
    let commonHtml = '<div class="elem__location"> <div class="location__head"> <span style="background-color:#E2F5EA;"><img src="/img/newImgs/geoicon.svg" alt="geoicon"></span> </div> <div class="location__content opened__location"> <div class="location__controls"> <a href="#"> <div class="controls__round"> <span class="round__minus"></span> <span class="round__plus"></span> </div> <p>Location <span>(Downtown)</span></p> </a> <div class="edit__controls">';
    let endCommonHtml = '</div></div></div>';
    let startInnerHtml = '<div class="regular__column--element">';
    let endInnerHtml = '</div>';
    let finalContent = '';
    for (var i = 0; i < jsonObj["Locations"].length; i++) {
        let editBtnHtml = '<a href="#" onclick="goToLocation(' + i + ')">EDIT <span><img src="/img/newImgs/editicon.svg" alt="editicon"></span></a> </div> </div> <div class="location__details" style="">';
        let locationEmailHtml = commonHtml + editBtnHtml + startInnerHtml + '<div class="el__column"><h6>Email</h6><p>' + jsonObj["Locations"][i].Email + '</p></div>';
        let locationNameHtml = '<div class="el__column"><h6>Name</h6><p>' + jsonObj["Locations"][i].Name + '</p></div>';
        let locationAddressHtml = '<div class="el__column"><h6>Address</h6><p>' + jsonObj["Locations"][i].Address + '</p></div>';
        let locationCityHtml = '<div class="el__column"><h6>City</h6><p>' + jsonObj["Locations"][i].City + '</p></div>';
        let locationProvinceHtml = '<div class="el__column"><h6>Province</h6><p>' + jsonObj["Locations"][i].Province + '</p></div>';
        let locationCountryHtml = '<div class="el__column"><h6>Country</h6><p>' + jsonObj["Locations"][i].Country + '</p></div>';
        let spacer = endInnerHtml + '<div class="spacer__column"></div>';

        let locationPostalHtml = startInnerHtml + '<div class="el__column"><h6>Postal</h6><p>' + jsonObj["Locations"][i].PostalCode + '</p></div>';
        let locationPhoneNoHtml = '<div class="el__column"><h6>Phone number</h6><p>' + jsonObj["Locations"][i].PhoneNo + '</p></div>';
        //let locationPrimaryContactNameHtml = '<div class="el__column"><h6>Primary Contact Name</h6><p>' + jsonObj["Locations"][i]["PostalCode"] + '</p></div>';
        let locationAnnualSalesVolumeHtml = '<div class="el__column"><h6>Annual Sales Volume</h6><p>' + jsonObj["Locations"][i].AnnualSalesVolume + '</p></div>';
        let locationTimeZoneHtml = '<div class="el__column"><h6>Timezone</h6><p>' + jsonObj["Locations"][i].TimeZone + '</p></div>' + endInnerHtml + endCommonHtml;

        finalContent = finalContent + locationEmailHtml + locationNameHtml + locationAddressHtml + locationCityHtml + locationProvinceHtml;
        finalContent += locationCountryHtml + spacer + locationPostalHtml + locationPhoneNoHtml;
        finalContent += locationAnnualSalesVolumeHtml + locationTimeZoneHtml;
    }
    $(".location__section").html(finalContent);
    $(".information__review").removeClass("d-none");
    $('html').animate({
        scrollTop: $(".information__review").offset().top
    }, 400);
})
//LocaitonInfo end
$(".contactInfo-editBtn").on("click", function (e) {
    e.preventDefault();
    $(".info__container").removeClass("d-none");
    $(".information__review").addClass("d-none");
    moveToContactInfoSection();
});
$(".businessInfo-editBtn").on("click", function (e) {
    e.preventDefault();
    $(".info__container").removeClass("d-none");
    $(".information__review").addClass("d-none");
    $(".switch1").css("display", "none");
    $(".switch2").fadeIn(300);
    $(".switch3").css("display", "none");
    $('.switch__list ul li').removeClass("current__switch");
    $(".switch__list ul li:nth-child(2)").addClass("current__switch");
    $('html').animate({
        scrollTop: $(".switch2").offset().top
    }, 400);
});
function goToLocation(index) {
    $(".info__container").removeClass("d-none");
    $(".information__review").addClass("d-none");
    $(".switch1").css("display", "none");
    $(".switch2").css("display", "none");
    $(".switch3").fadeIn(300);
    $('.switch__list ul li').removeClass("current__switch");
    $(".switch__list ul li:nth-child(3)").addClass("current__switch");
    $('html').animate({
        scrollTop: $(".locationNo-" + index).offset().top
    }, 400);
}
function createBusinessInfoJson() {
    let id = $("#businessInfoId").val().trim();
    id = id == null || id == "" ? null : id;
    let productAndServices = $(".businessInfo_ProductServices").text().trim();
    let buyingGroup = $(".businessInfo_BuyingGroup").text().trim();
    buyingGroup = buyingGroup != "Select all that apply" ? buyingGroup : "";
    let currencyId = $(".businessInfo_Currency").children().attr("data-value");
    let currency = $(".businessInfo_Currency").children().text().trim();
    productAndServices = productAndServices != "Select all that apply" ? productAndServices : "";
    let prodAndServiceIds = createProdAndServicesList();
    let buyingGroupIds = createBuyingGroupList();
    let businessInfo = {
        "Id": id,
        "StoreName": $("#StoreName").val().trim(),
        "PartnerId": $("#PartnerId").val().trim(),
        "BusinessLegalName": $("#BusinessLegalName").val().trim(),
        "CurrencyId": currencyId == null || currencyId == "" ? null : currencyId,
        "Currency": currency.trim(),
        "ProductAndServices": productAndServices.trim(),
        "ProductAndServiceIds": prodAndServiceIds,
        "BuyingGroup": buyingGroup.trim(),
        "BuyingGroupIds": buyingGroupIds
    }
    return businessInfo;
}
function createContactInfoJson() {
    let id = $("#contactInfoId").val().trim();
    id = id == null || id == "" ? null : id;
    let langId = $(".contactInfo_Language").children().attr("data-value");
    let lang = $(".contactInfo_Language").children().text().trim();
    let contactInfo = {
        "Id": id,
        "FirstName": $("#FirstName").val().trim(),
        "LastName": $("#LastName").val().trim(),
        "Email": $("#Email").val().trim(),
        "PhoneNumber": $("#PhoneNumber").val().trim(),
        "LanguageId": langId == null || langId == "" ? null : langId,
        "Language": lang
    }
    return contactInfo;
}
function createLocationInfoJson() {
    let rowNo = $('.locationRow').length;
    var annualSalesVolumeIds = $('.annualSalesVolumeRadio:checked').map(function () {
        return this.value;
    }).get();
    console.log(annualSalesVolumeIds);
    let locationInfoArray = [];
    for (var i = 0; i < rowNo; i++) {
        let locationName = $("#LocationName-" + i).val().trim();
        let subMarketPartnerId = $("#subMarketPartnerId-" + i).val();
        let locationId = $("#locationInfoId-" + i).val();
        //locationName = locationName != "Select Location Name" ? locationName : "";
        let annualSalesVolume = $("#LocationAnnualSalesVolume-" + i).text().trim();
        annualSalesVolume = annualSalesVolume != "Select sales volume range" ? annualSalesVolume : "";
        let annualSalesVolumeId = $(".annualSalesVolumeDropDown-" + i).find("input:checked").val();
        console.log("annualSalesVolumeId", annualSalesVolumeId);
        let locationInfo = {
            "Id": locationId == null || locationId == "" ? null : locationId,
            "SubBusinessInfoId": subMarketPartnerId == null || subMarketPartnerId == "" ? null : subMarketPartnerId,
            "Name": locationName.trim(),
            "City": $("#LocationCity-" + i).val().trim(),
            "AnnualSalesVolume": annualSalesVolume.trim(),
            "AnnualSalesVolumeId": annualSalesVolumeId == null ? null : annualSalesVolumeId,
            "Province": $("#LocationProvince-" + i).val().trim(),
            "ProvinceCode": $("#ProvinceCode-" + i).val().trim(),
            "Email": $("#LocationEmail-" + i).val().trim(),
            "PostalCode": $("#PostalCode-" + i).val().trim(),
            "PhoneNo": $("#LocationPhoneNo-" + i).val().trim(),
            //"PhoneCode": $("#LocationPhoneCode-" + i).text(),
            "Country": $("#LocationCountry-" + i).val().trim(),
            "Address": $("#Address-" + i).val().trim(),
            "TimeZone": $("#LocationTimeZone-" + i).text().trim(),
            "TimeZoneId": $("#LocationTimeZone-" + i).attr("data-timezoneId")
        };
        locationInfoArray.push(locationInfo);
    }
    return locationInfoArray;
}
//validations of ContactInfo start
function checkContactFirstName() {
    let val1 = $("#FirstName").val().trim();
    if (val1 == null || val1 == "") {
        $(".contactFirstNameValidationField").removeClass("d-none");
        return false;
    }
    else {
        $(".contactFirstNameValidationField").addClass("d-none");
        return true;
    }
}
function checkContactLastName() {
    let val1 = $("#LastName").val().trim();
    if (val1 == null || val1 == "") {
        $(".contactLastNameValidationField").removeClass("d-none");
        return false;
    }
    else {
        $(".contactLastNameValidationField").addClass("d-none");
        return true;
    }
}
function checkContactEmail() {
    let val = $("#Email").val().trim();
    if (IsEmail(val)) {
        $(".contactEmailValidationField").addClass("d-none");
        return true;
    }
    else {
        $(".contactEmailValidationField").removeClass("d-none")
        val = "";
        return false;
    }

}
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}
function checkContactPhoneNumber() {
    let val1 = $("#PhoneNumber").val().trim();
    if (val1 == null || val1 == "") {
        $(".contactPhoneValidationField").removeClass("d-none");
        return false;
    }
    else {
        $(".contactPhoneValidationField").addClass("d-none");
        return true;
    }
}
function validateAllContactInfoFields() {
    let isValid = $("#frmContactInfo").valid();
    let isPhoneNoValid = checkContactPhoneNumber();
    if (isValid && isPhoneNoValid)
        return true;
    else
        return false;
}
function validateAllBusinessInfoFields() {
    let isValid = $("#frmBusinessInfo").valid();
    if (isValid)
        return true;
    else
        return false;
}

//validations of ContactInfo end

function isWholeNumber(txt, evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;

    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

//validations of LocationInfo start
function checkLocationPhoneNumber(index) {
    let val1 = $("#LocationPhoneNo-" + index).val().trim();
    if (val1 == null || val1 == "") {
        $(".locationPhoneValidationField-" + index).removeClass("d-none");
        return false;
    }
    else {
        $(".locationPhoneValidationField-" + index).addClass("d-none");
        return true;
    }
}
function validateAllLocationInfoFields(index) {
    //$('.locationAddress').each(function () {
    //    $(".locationAddress").rules("add",
    //        {
    //            required: true,
    //            messages: {
    //                required: "Address is required",
    //            }
    //        });
    //});
    let isValid = $("#frmLocationInfo").valid();
    //console.log("frmLocationInfo",isValid);
    let isPhoneNoValid = checkLocationPhoneNumber(index);

    if (isPhoneNoValid)
        return true;
    else
        return false;
}
//validations of LocationInfo end

//restrict user movement on tabs
$(".switch__list ul li a").on("click", function (e) {
    e.preventDefault();
    var index = $(this).parent().index();
    //let isContactInfoValidated = false;
    //let isBusinessInfoValidated = false;
    //console.log("index", index)
    //console.log("currentindex", $(this).closest(".switch__list").find(".current__switch").index())
    //let currentIndex = $(this).closest(".switch__list").find(".current__switch").index();
    //if (currentIndex == 0)
    //    isContactInfoValidated = validateAllContactInfoFields();
    //else if (currentIndex == 1)
    //    isBusinessInfoValidated = validateAllBusinessInfoFields();

    //console.log("isBusinessInfoValidated", isBusinessInfoValidated)
    if (validateAllContactInfoFields() && validateAllBusinessInfoFields()) {
        $(".switch__container").css("display", "none");
        $(".switch__container." + $(this).attr("data-switch")).fadeIn(300);
        $(this).closest(".switch__list").find("li").removeClass("current__switch");
        console.log($(this).closest("li").html())
        $(this).closest("li").addClass("current__switch");
    }
    else if (validateAllContactInfoFields() && index <= 1) {
        $(".switch__container").css("display", "none");
        $(".switch__container." + $(this).attr("data-switch")).fadeIn(300);
        $(this).closest(".switch__list").find("li").removeClass("current__switch");
        $(this).closest("li").addClass("current__switch");
    }
});
//end


$(document).on('click', '.verifySubmit', function (e) {
    e.preventDefault();
    //$("#msgNotifierBody").html("Please Wait");
    //$("#msgModal").modal();
    $(".verifySubmit").addClass("d-none");
    $(".loader2").removeClass("d-none");
    $.ajax({
        type: 'POST',
        data: jsonObj,
        url: '/SmartPartnerOnBoarding/SmarterPartnerData',
        success: function (result) {

            if (typeof (result) != 'string') {
                $("#contactInfoId").val(result.ContactInfoId);
                $("#businessInfoId").val(result.BusinessInfoId);
                console.log("businessInfoId", $("#businessInfoId").val());
                console.log("contactInfoId",$("#contactInfoId").val());
                $(".locationKeyList").html("");
                for (var i = 0; i < result.LocationInfoIds.length; i++) {
                    let val = result.LocationInfoIds[i];
                    let key = "locationInfoId-" + i;
                    $(".locationKeyList").append("<input hidden id='" + key + "' name='" + key +"' value='"+val+"' />");
                }
                $(".subBusinessInfoKeyList").html("");
                for (var i = 0; i < result.SubMarketPartnerIds.length; i++) {
                    let val = result.SubMarketPartnerIds[i];
                    let key = "subMarketPartnerId-" + i;
                    $(".subBusinessInfoKeyList").append("<input hidden id='" + key + "' name='" + key +"' value='"+val+"' />");
                }
                $("#msgModal").modal("hide");
                $(".information__review").addClass("d-none");
                $(".congrats__block").removeClass("d-none");
            }
            else {
                $("#msgNotifierBody").html(result);
                $("#msgModal").modal();
            }
            $(".verifySubmit").removeClass("d-none");
            $(".loader2").addClass("d-none");
            console.log(result)
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
})
$(".edit__Info").click(function () {
    $(".info__container").removeClass("d-none");
    $(".congrats__block").addClass("d-none");
    moveToContactInfoSection();
})
function moveToContactInfoSection() {
    $(".switch1").fadeIn(300);
    $(".switch2").css("display", "none");
    $(".switch3").css("display", "none");
    $('.switch__list ul li').removeClass("current__switch");
    $(".switch__list ul li:nth-child(1)").addClass("current__switch");
    $('html').animate({
        scrollTop: $(".switch1").offset().top
    }, 400);
}



//location googleapi
function addressChange(index) {
    onAddressEmpty(index);
    const options = {
        componentRestrictions: { country: ["us", "ca"] },
    };
    var autocomplete = new google.maps.places.Autocomplete($("#Address-" + index)[0], options);
    google.maps.event.addListener(autocomplete, 'place_changed', function () {

        var place = autocomplete.getPlace();
        console.log(place)
        console.log(place.address_components);

        const types = {
            administrative_area_level_1: 'province',
            locality: 'city',
            country: 'country',
            postal_code: 'postalCode'
        };

        const result = {};

        if (place.address_components) {
            if (place.address_components) {
                place.address_components.map(address => {
                    if (address.types[0]) {
                        console.log(address.types[0])
                        if (types[address.types[0]]) {
                            if (address.types[0] == "administrative_area_level_1") {
                                result["provinceCode"] = address.short_name;
                            }
                            result[types[address.types[0]]] = address.long_name;
                        }
                    }
                });
            }
        }
        result.city = result.city == null ? result.province : result.city
        $("#LocationCity-" + index).val(result.city);
        $("#LocationProvince-" + index).val(result.province);
        $("#LocationCountry-" + index).val(result.country);
        $("#PostalCode-" + index).val(result.postalCode);
        $("#ProvinceCode-" + index).val(result.provinceCode);
        console.log(result);
        $("#frmLocationInfo").valid();
        var key = "AIzaSyA7QegStMXvxJBDUUi-54a37TcPvgsC7i8";
        var latitude = place.geometry.location.lat();
        var longitude = place.geometry.location.lng();
        var weburl = "https://maps.googleapis.com/maps/api/timezone/json?location=" + latitude + "," + longitude + "&key=" + key + "&timestamp=" + (Math.round((new Date().getTime()) / 1000)).toString()/* + "&sensor=false"*/;
        $.ajax({
            type: "POST",
            url: weburl,
            dataType: "json",
            success: function (result) {
                let labels = [];
                $($('.locationTimezoneDropDown')).each(function () {// id of ul
                    var li = $(this).find('li')//get each li in ul
                    li.each(function () {// id of ul
                        var name = $(this).find(".tz__text").text();
                        var id = $(this).find(".tz__text").attr("data-timezone");
                        labels.push({ "TimeZoneId": id, "TimeZone": name });
                    })
                })
                let timeZoneObj = labels.find(el => el.TimeZone === result.timeZoneName);
                if (timeZoneObj != null) {
                    $("#LocationTimeZone-" + index).html(result.timeZoneName);
                    $("#LocationTimeZone-" + index).attr("data-timezoneId", timeZoneObj.TimeZoneId);
                }
                else
                    $("#LocationTimeZone-" + index).text("");
            }
        });
    });
}


//make country city province postal code timezone empty if address is empty
function onAddressEmpty(index) {
    let addr = $("#Address-" + index).val().trim();
    if (addr == null || addr == "") {
        $("#LocationCity-" + index).val("");
        $("#LocationProvince-" + index).val("");
        $("#LocationCountry-" + index).val("");
        $("#PostalCode-" + index).val("");
        $("#LocationTimeZone-" + index).text("");
        $("#frmLocationInfo").valid();
    }
}

function createProdAndServicesList() {
    let prodAndServices = [];
    let count = $(".prodAndServices li").length;
    for (var index = 0; index < count; index++) {
        if ($("#prodService-" + index).prop("checked")) {
            let valueId = $("#prodServiceId-" + index).val();
            prodAndServices.push(valueId)
        }
    }
    return prodAndServices;
}
function createBuyingGroupList() {
    let buyingGroup = [];
    let count = $(".buyingGroup li").length;
    for (var index = 0; index < count; index++) {
        if ($("#buyingGroup-" + index).prop("checked")) {
            let valueId = $("#buyingGroupId-" + index).val();
            buyingGroup.push(valueId)
        }
    }
    return buyingGroup;
}
function pasteToLocationName() {
    let storeName = $("#StoreName").val();
    $(".locationName").val(storeName);
}

$(".getMarketPartner").click(function (e) {
    e.preventDefault();
    let partnerId = $("#existingPartnerId").val();
    $.ajax({
        type: "GET",
        url: '/SmartPartnerOnBoarding/BusinessInfo',
        data: { partnerId: partnerId },
        success: function (result) {
            $("#businessInfoId").val(result.Id);
            $("#StoreName").val(result.StoreName);
            $("#PartnerId").val(result.PartnerId);
            $("#BusinessLegalName").val(result.BusinessLegalName);
            pasteToLocationName();
            let i = 0;
            $('.buyingGroup').each(function () {// id of ul
                var li = $(this).find('li')//get each li in ul
                li.each(function () {
                    var id = $("#buyingGroupId-" + i).val();
                    if (result.BuyingGroupIds.includes(parseInt(id))) {
                        $("#buyingGroup-" + i).prop("checked", true);
                    }
                    ++i;
                })
            });
            i = 0;
            $('.prodAndServices').each(function () {// id of ul
                var li = $(this).find('li')//get each li in ul
                li.each(function () {
                    var id = $("#prodServiceId-" + i).val();
                    if (result.ProductAndServiceIds.includes(parseInt(id))) {
                        $("#prodService-" + i).prop("checked", true);
                    }
                    ++i;
                })
            });
            i = 0;
            $('.currencyDropDown').each(function () {// id of ul
                var li = $(this).find('li')//get each li in ul
                li.each(function (index, elem) {
                    var id = $(elem).find(".tz__text").children().attr("data-value");
                    if (id == parseInt(result.CurrencyId)) {
                        $(".businessInfo_Currency").html($(elem).find(".tz__text").html());
                        return;
                    }
                    ++i;
                })
            });
            var newText = "";
            $(".prodAndServices>li").each(function (index, elem) {
                if ($(elem).find("input")) {
                    if (!$(elem).find("input").hasClass("other__input")) {
                        if ($(elem).find("input").is(":checked")) {
                            if (newText == "" || newText == " ") {
                                newText += $(elem).find(".form-group").attr("data-value");
                                $(".businessInfo_ProductServices").text(newText);
                            } else {
                                newText += "," + $(elem).find(".form-group").attr("data-value");
                                $(".businessInfo_ProductServices").text(newText);
                            }
                        }
                    }
                }
            });
            newText = "";
            $(".buyingGroup>li").each(function (index, elem) {
                if ($(elem).find("input")) {
                    if (!$(elem).find("input").hasClass("other__input")) {
                        if ($(elem).find("input").is(":checked")) {
                            if (newText == "" || newText == " ") {
                                newText += $(elem).find(".form-group").attr("data-value");
                                $(".businessInfo_BuyingGroup").text(newText);
                            } else {
                                newText += "," + $(elem).find(".form-group").attr("data-value");
                                $(".businessInfo_BuyingGroup").text(newText);
                            }
                        }
                    }
                }
            });
            $("#frmBusinessInfo").valid();
        }
    })

})
$("#partnerCheckbox").click(function () {
    if ($("#partnerCheckbox").prop("checked")) {
        $("#existingPartnerId").prop("disabled", false);
    }
    else
    {
        $("#existingPartnerId").prop("disabled", true);
    }
});
