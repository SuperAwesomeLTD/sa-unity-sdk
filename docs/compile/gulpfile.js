var gulp = require('gulp');
var gutil = require('gulp-util');
var plumber = require('gulp-plumber');

var less = require('gulp-less');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var watch = require('gulp-watch');
var gulpImports = require('gulp-imports');
var nodemon = require('gulp-nodemon');
var path = require('path');
var rename = require('gulp-rename');

var docs_to_json = require('sa-docs-to-json');

gulp.task('default', function(){
    gulp.start('docs');
})

gulp.task('docs', function() {
    gulp.src('../*.json')
    .pipe(docs_to_json())
    .pipe(gulp.dest('./'))
});