"use strict";

var gulp = require("gulp"),
    path = require('path'),
    copyResources = require('./node_modules/@jellog/aspnetcore.mvc.ui/gulp/copy-resources.js');

exports.default = function(){
    return copyResources(path.resolve('./'));
};