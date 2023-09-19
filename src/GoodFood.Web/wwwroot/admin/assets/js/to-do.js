! function (e) {
  e(document).ready(function () {
      var s, a = function (a, t, l) {
              var o = e(".notification-popup ");
              o.find(".task").text(a), o.find(".notification-text").text(t), o.removeClass("hide success"), l && o.addClass(l), s && clearTimeout(s), s = setTimeout(function () {
                  o.addClass("hide")
              }, 3e3)
          },
          t = function () {
              var s = e("#new-task").val();
              if ("" == s) e("#new-task").addClass("error"), e(".new-task-wrapper .error-message").removeClass("hidden");
              else {
                  var t = e(".todo-list-body").prop("scrollHeight"),
                      l = e(o).clone();
                  l.find(".task-label").text(s), l.addClass("new"), l.removeClass("completed"), e("#todo-list").append(l), e("#new-task").val(""), e("#mark-all-finished").removeClass("move-up"), e("#mark-all-incomplete").addClass("move-down"), a(s, " به لیست اضافه شد"), e(".todo-list-body").animate({
                      scrollTop: t
                  }, 1e3)
              }
          },
          l = function () {
              e(".add-task-btn").toggleClass("hide"), e(".new-task-wrapper").toggleClass("visible"), e("#new-task").hasClass("error") && (e("#new-task").removeClass("error"), e(".new-task-wrapper .error-message").addClass("hidden"))
          },
          o = e(e("#task-template").html());
      e(".add-task-btn").click(function () {
          var s = e(".new-task-wrapper").offset().top;
          e(this).toggleClass("hide"), e(".new-task-wrapper").toggleClass("visible"), e("#new-task").focus(), e("body").animate({
              scrollTop: s
          }, 1e3)
      }), e("#todo-list").on("click", ".task-action-btn .delete-btn", function () {
          var s = e(this).closest(".task"),
              t = s.find(".task-label").text();
          s.remove(), a(t, "  حذف شده است.")
      }), e("#todo-list").on("click", ".task-action-btn .complete-btn", function () {
          var s = e(this).closest(".task"),
              t = s.find(".task-label").text(),
              l = s.hasClass("completed") ? "علامت گذاری انجام شده" : "علامت گذاری انجام نشده";
          e(this).attr("title", l), s.hasClass("completed") ? a(t, "به عنوان انجام نشده علامت گذاری شد.") : a(t, "به عنوان انجام شده علامت گذاری شد.", "success"), s.toggleClass("completed")
      }), e("#new-task").keydown(function (s) {
          var a = s.keyCode,
              o = 13,
              n = 27;
          e("#new-task").hasClass("error") && (e("#new-task").removeClass("error"), e(".new-task-wrapper .error-message").addClass("hidden")), a == o ? (s.preventDefault(), t()) : a == n && l()
      }), e("#add-task").click(t), e("#close-task-panel").click(l), e("#mark-all-finished").click(function () {
          e("#todo-list .task").addClass("completed"), e("#mark-all-incomplete").removeClass("move-down"), e(this).addClass("move-up"), a("همه وظایف", "به عنوان انجام شده علامت گذاری شده است.", "success")
      }), e("#mark-all-incomplete").click(function () {
          e("#todo-list .task").removeClass("completed"), e(this).addClass("move-down"), e("#mark-all-finished").removeClass("move-up"), a("همه وظایف", "به عنوان انجام نشده علامت گذاری شده است.")
      })
  })
}(jQuery);