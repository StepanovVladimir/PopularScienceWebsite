import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Category } from 'src/app/models/category';
import { ArticlesService } from 'src/app/services/articles.service';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.scss']
})
export class EditArticleComponent implements OnInit {

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

  id: number
  articleGotten: boolean = false
  categoryIds: number[]
  form: FormGroup
  categories: Category[]

  constructor(
    private route: ActivatedRoute,
    private articlesService: ArticlesService,
    private categoriesService: CategoriesService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.categoriesService.getCategories().subscribe(res => {
      this.categories = res
    })

    this.id = this.route.snapshot.params.id
    this.articlesService.getArticleWithCategoryIds(this.id).subscribe(res => {
      this.categoryIds = res.categoryIds
      this.form = this.formBuilder.group({
        categoryIds: new FormArray(res.categoryIds.map(ci => new FormControl(ci))),
        title: [res.title, [Validators.required]],
        image: [null],
        description: [res.description, [Validators.required]],
        content: [res.content, [Validators.required]]
      })
      this.articleGotten = true
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

  updateArticle() {
    let formData = new FormData();
    formData.append('title', this.form.value.title)
    if (this.form.value.image) {
      formData.append('image', this.form.value.image)
    }
    formData.append('description', this.form.value.description)
    formData.append('content', this.form.value.content)

    let formArray = this.form.get('categoryIds') as FormArray
    formArray.controls.forEach(ctrl => formData.append('categoryIds', ctrl.value))

    this.articlesService.updateArticle(this.id, formData).subscribe(res => {
      this.router.navigate(['/article/show', this.id])
    }, error => {
      alert("Не удалось обновить статью")
    })
  }
}
