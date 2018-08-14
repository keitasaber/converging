/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.extraPlugins = 'easyimage';
    config.cloudServices_tokenUrl = '/Assets/Admin/ckeditor/cloudservices/plugin.js';
    config.cloudServices_uploadUrl = '/Assets/Admin/ckeditor/cloudservices/plugin.js';
    config.filebrowserBrowseUrl = '/Assets/Admin/ckfinder/ckfinder.html';
    config.filebrowserUploadUrl = '/Assets/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=files';
    config.filebrowserWindowWidth = '1000';
    config.filebrowserWindowHeight = '700';
};
