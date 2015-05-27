/// <reference path="jquery-1.2.6.min.js" />
/// jQuery plugin to add support for SwfUpload
/// (c) 2008 Steven Sanderson

(function($) {
    $.fn.makeAsyncUploader = function(options) {
        return this.each(function() {
            // Put in place a new container with a unique ID
            var id = $(this).attr("id");
            var responseTemp;
            var container = $("<div class='divUploader'>");
            container.append($("<div id='" + id + "_divFoto' class='divFoto'>Foto</div>"));
            container.append($("<span class='asyncUploader'/>"));
            container.append($("<span id='" + id + "_swf'/>"));
            container.append($("<span id='" + id + "_uploading'>Enviando... <input type='button' class='btnCancel' value='Cancel'/></span>"));
            container.append($("<span id='" + id + "_completedMessage'></span>"));
            container.append($("<input type='button' name='" + id + "_delete' id='" + id + "_delete' value='Excluir'/>"));
            container.append($("<div class='progress'><div class='ProgressBar'> <div>&nbsp;</div> </div></div>"));
            container.append($("<input type='hidden' name='" + id + "_filename'/>"));
            container.append($("<input type='hidden' name='" + id + "_guid'/>"));
            container.append($("</div>"));

            $(this).before(container).remove();
            $("div.ProgressBar", container).hide();
            $("span[id$=_uploading]", container).hide();

            $("#" + id + "_delete").click(function() {
                upload_delete_file(options.file_name, id);
            });

            if (options.file_name != undefined) {
                $("#" + id + "_divFoto").html("<img width=\"80\" src=\"" + options.file_name + "\">");
                $("#" + id + "_divFoto").show(500);
            }

            // Instantiate the uploader SWF
            var swfu;
            var width = 109, height = 22;
            if (options) {
                width = options.width || width;
                height = options.height || height;
            }
            var defaults = {
                flash_url: "swfupload.swf",
                upload_url: "Uploads",
                file_size_limit: "3 MB",
                file_types: "*.*",
                file_types_description: "All Files",
                debug: false,

                button_image_url: "blankButton.png",
                button_width: width,
                button_height: height,
                button_placeholder_id: id + "_swf",
                button_text: "<font face='verdana' size='11px'>&nbsp;&nbsp;Arquivo</font>",
                button_text_left_padding: (width - 70) / 2,
                button_text_top_padding: 1,

                // Called when the user chooses a new file from the file browser prompt (begins the upload)
                file_queued_handler: function(file) { swfu.startUpload(); },

                // Called when a file doesn't even begin to upload, because of some error
                file_queue_error_handler: function(file, code, msg) { alert("Desculpe, o arquivo não pode ser enviado: " + msg); },

                // Called when an error occurs during upload
                upload_error_handler: function(file, code, msg) { alert("Desculpe, o arquivo não pode ser enviado: " + msg); },

                // Called when upload is beginning (switches controls to uploading state)
                upload_start_handler: function() {
                    swfu.setButtonDimensions(0, height);
                    $("input[name$=_filename]", container).val("");
                    $("input[name$=_guid]", container).val("");
                    $("div.ProgressBar div", container).css("width", "0px");
                    $("div.ProgressBar", container).show();
                    $("span[id$=_uploading]", container).show();
                    $("span[id$=_completedMessage]", container).html("").hide();

                    if (options.disableDuringUpload)
                        $(options.disableDuringUpload).attr("disabled", "disabled");
                },

                // Called when upload completed successfully (puts success details into hidden fields)
                upload_success_handler: function(file, response) {
                    $("input[name$=_filename]", container).val(file.name);
                    $("input[name$=_guid]", container).val(response);
                    responseTemp = response;
                    $("span[id$=_completedMessage]", container).html("<b>{0}</b> ({1} KB)"
                        .replace("{0}", file.name)
                        .replace("{1}", Math.round(file.size / 1024))
                    );

                },

                // Called when upload is finished (either success or failure - reverts controls to non-uploading state)
                upload_complete_handler: function() {
                    var clearup = function() {
                        $("div.ProgressBar", container).hide();
                        $("span[id$=_completedMessage]", container).show();
                        $("span[id$=_uploading]", container).hide();
                        swfu.setButtonDimensions(width, height);
                        //alert(responseTemp);

                        $.ajax({
                            type: "POST",
                            url: "../File.aspx/ImageInfo",
                            data: "{}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            error: function(e) {
                                alert(e.toString());
                            },
                            success: function(r) {
                                if (r.Extension == "jpg" || r.Extension == "gif" || r.Extension == "png") {
                                    $("#" + id + "_divFoto").html("<img width=\"80\" src=\"" + r.Url + "\">");
                                    $("#" + id + "_divFoto").show(500);
                                }
                                else {
                                    $("#" + id + "_divFoto").hide(500);
                                }
                                $("#" + id + "_delete").click(function() {
                                    upload_delete_file(r.Url, id);
                                });
                            }
                        });
                    };
                    if ($("input[name$=_filename]", container).val() != "") // Success
                        $("div.ProgressBar div", container).animate({ width: "100%" }, { duration: "fast", queue: false, complete: clearup });
                    else // Fail
                        clearup();

                    if (options.disableDuringUpload)
                        $(options.disableDuringUpload).removeAttr("disabled");
                },

                // Called periodically during upload (moves the progess bar along)
                upload_progress_handler: function(file, bytes, total) {
                    var percent = 100 * bytes / total;
                    $("div.ProgressBar div", container).animate({ width: percent + "%" }, { duration: 500, queue: false });
                }
            };
            swfu = new SWFUpload($.extend(defaults, options || {}));

            // Called when user clicks "cancel" (forces the upload to end, and eliminates progress bar immediately)
            $("span[id$=_uploading] input[type='button']", container).click(function() {
                swfu.cancelUpload(null, false);
            });

            // Give the effect of preserving state, if requested
            if (options.existingFilename || "" != "") {
                $("span[id$=_completedMessage]", container).html("Uploaded <b>{0}</b> ({1} KB)"
                                .replace("{0}", options.existingFilename)
                                .replace("{1}", options.existingFileSize ? Math.round(options.existingFileSize / 1024) : "?")
                            ).show();
                $("input[name$=_filename]", container).val(options.existingFilename);
            }
            if (options.existingGuid || "" != "")
                $("input[name$=_guid]", container).val(options.existingGuid);
        });

    }


})(jQuery);

function upload_delete_file(file, objId) {
    $.ajax({
        type: "POST",
        url: "../File.aspx/DeleteFile",
        data: "{'path':'" + file + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function(e) {
            alert(e.toString());
        },
        success: function(msg) {
            $("#" + objId + "_divFoto").hide(500);
            $("#" + objId + "_completedMessage").hide();
        }
    });
}