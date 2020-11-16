import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Category } from 'src/app/models/category';
import { ArticlesService } from 'src/app/services/articles.service';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-create-article',
  templateUrl: './create-article.component.html',
  styleUrls: ['./create-article.component.scss']
})
export class CreateArticleComponent implements OnInit {

  editorConfig: AngularEditorConfig = {
    editable: true,
      spellcheck: true,
      height: 'auto',
      minHeight: '100',
      maxHeight: 'auto',
      width: 'auto',
      minWidth: '0',
      translate: 'yes',
      enableToolbar: true,
      showToolbar: true,
      defaultParagraphSeparator: '',
      defaultFontName: '',
      defaultFontSize: '',
      fonts: [
        {class: 'arial', name: 'Arial'},
        {class: 'times-new-roman', name: 'Times New Roman'},
        {class: 'calibri', name: 'Calibri'},
        {class: 'comic-sans-ms', name: 'Comic Sans MS'}
      ],
      customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    uploadWithCredentials: false,
    sanitize: true,
    toolbarPosition: 'top',
    toolbarHiddenButtons: [
      ['insertImage', 'insertVideo']
    ]
};

  form: FormGroup
  categories: Category[]

  constructor(
    private articlesService: ArticlesService,
    private categoriesService: CategoriesService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.categoriesService.getCategories().subscribe(res => {
      this.categories = res
    })

    this.form = this.formBuilder.group({
      categoryIds: new FormArray([]),
      title: ['', [Validators.required]],
      image: [null, [Validators.required]],
      description: ['', [Validators.required]],
      content: ['', [Validators.required]]
    })
  }

  checkChange(event) {
    let formArray = this.form.get('categoryIds') as FormArray;

    if (event.target.checked) {
      formArray.push(new FormControl(event.target.value))
    } else {
      formArray.controls.forEach((ctrl, i) => {
        if (ctrl.value == event.target.value) {
          formArray.removeAt(i);
          return;
        }
      });
    }
  }

  selectFile(event) {
    if (event.target.files.length > 0) {
      let file = <File>event.target.files[0]
      this.form.get('image').setValue(file)
    }
  }

  createArticle() {
    let formData = new FormData();
    formData.append('title', this.form.value.title)
    formData.append('image', this.form.value.image)
    formData.append('description', this.form.value.description)
    formData.append('content', this.form.value.content)

    let formArray = this.form.get('categoryIds') as FormArray
    formArray.controls.forEach(ctrl => formData.append('categoryIds', ctrl.value))

    this.articlesService.createArticle(formData).subscribe(res => {
      this.router.navigate(['/article/show', res.id])
    }, error => {
      alert("Не удалось добавить статью")
    })
  }
}
