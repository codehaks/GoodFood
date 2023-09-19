// npm package: jquery-validation
// github link: https://github.com/jquery-validation/jquery-validation

$(function() {
  'use strict';

  $.validator.setDefaults({
    submitHandler: function() {
      alert("ارسال شد!");
    }
  });
  $(function() {
    // validate signup form on keyup and submit
    $("#signupForm").validate({
      rules: {
        name: {
          required: true,
          minlength: 3
        },
        email: {
          required: true,
          email: true
        },
        age_select: {
          required: true
        },
        gender_radio: {
          required: true
        },
        skill_check: {
          required: true
        },
        password: {
          required: true,
          minlength: 5
        },
        confirm_password: {
          required: true,
          minlength: 5,
          equalTo: "#password"
        },
        terms_agree: "required"
      },
      messages: {
        name: {
          required: "لطفا نام خود را وارد کنید",
          minlength: "نام نباید کمتر از 3 کاراکتر باشد"
        },
        email: "لطفا یک آدرس ایمیل معتبر وارد کنید",
        age_select: "لطفا سن خود را انتخاب کنید",
        skill_check: "لطفا مهارت خود را انتخاب کنید",
        gender_radio: "لطفا جنسیت خود را انتخاب کنید",
        password: {
          required: "لطفا رمز عبور خود را وارد کنید",
          minlength: "طول رمز عبور نباید کمتر از 5 کاراکتر باشد"
        },
        confirm_password: {
          required: "لطفا رمز عبور خود را وارد کنید",
          minlength: "طول رمز عبور نباید کمتر از 5 کاراکتر باشد",
          equalTo: "لطفا تکرار رمز عبور را صحیح وارد کنید"
        },
        terms_agree: "لطفا با قوانین و مقررات موافقت کنید"
      },
      errorPlacement: function(error, element) {
        error.addClass( "invalid-feedback" );

        if (element.parent('.input-group').length) {
          error.insertAfter(element.parent());
        }
        else if (element.prop('type') === 'radio' && element.parent('.radio-inline').length) {
          error.insertAfter(element.parent().parent());
        }
        else if (element.prop('type') === 'checkbox' || element.prop('type') === 'radio') {
          error.appendTo(element.parent().parent());
        }
        else {
          error.insertAfter(element);
        }
      },
      highlight: function(element, errorClass) {
        if ($(element).prop('type') != 'checkbox' && $(element).prop('type') != 'radio') {
          $( element ).addClass( "is-invalid" ).removeClass( "is-valid" );
        }
      },
      unhighlight: function(element, errorClass) {
        if ($(element).prop('type') != 'checkbox' && $(element).prop('type') != 'radio') {
          $( element ).addClass( "is-valid" ).removeClass( "is-invalid" );
        }
      }
    });
  });
});